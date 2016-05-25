using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tinsel
{
    class Controller
    {
        private List<string>        m_knownNodes    = new List<string>();

        private List<int>           m_requestedIDs  = new List<int>();
        private List<RecieveObject> m_requestedObj  = new List<RecieveObject>();
        private List<RecieveObject> m_finishedObj   = new List<RecieveObject>();
        

        private UdpClient udpClient;

        private int m_srcPort;

        private bool m_bouncing = false;

        private Thread m_workerThread;


        public void AddRequest( int id)
        {
            m_requestedIDs.Add(id);
            m_requestedObj.Add(new RecieveObject(id));
        }

        public void AddNode(string _ip)
        {
            m_knownNodes.Add(_ip);
        }

        public void InitClient(int _srcPort)
        {
            m_srcPort = _srcPort;
            udpClient = new UdpClient();
        }

        public void SendUdp(string dstIp, int dstPort, byte[] data)
        {
            udpClient.Send(data, data.Length, dstIp, dstPort);
        }

        public bool SendPacket(TransferPacket _packet)
        {
            bool anySent = false;
            for ( int i = 0; i < m_knownNodes.Count(); i++)
            {
                //Send packet
                int[] intArray = { _packet.object_id, _packet.packet_id, _packet.size, _packet.data };

                byte[] bytePacket = new byte[intArray.Length * sizeof(int)];
                Buffer.BlockCopy(intArray, 0, bytePacket, 0, bytePacket.Length);

                SendUdp(m_knownNodes[i], 11000, bytePacket);
            }

            return anySent;
        }

        public bool SendPacket(byte[] _packet)
        {
            bool anySent = false;
            for (int i = 0; i < m_knownNodes.Count(); i++)
            {
                SendUdp("127.0.0.1", 11000, _packet);
            }

            return anySent;
        }

        public void StartBouncing()
        {
            m_bouncing = true;
            m_workerThread = new Thread(BounceFunction);

            m_workerThread.Start();
            while (!m_workerThread.IsAlive) ;
        }

        public void StopBouncing()
        {
            m_bouncing = false;
            m_workerThread.Join();
        }

        public void BounceFunction()
        {
            //Creates a UdpClient for reading incoming data.
            UdpClient receivingUdpClient = new UdpClient(11000);

            while (m_bouncing)
            {
                //Creates an IPEndPoint to record the IP Address and port number of the sender. 
                // The IPEndPoint will allow you to read datagrams sent from any source.
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                try
                {
                    // Blocks until a message returns on this socket from a remote host.
                    Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                    string returnData = Encoding.ASCII.GetString(receiveBytes);

                    int[] recievePacket = new int[4];

                    recievePacket[0] = BitConverter.ToInt32(receiveBytes, 0);
                    recievePacket[1] = BitConverter.ToInt32(receiveBytes, 4);
                    recievePacket[2] = BitConverter.ToInt32(receiveBytes, 8);
                    recievePacket[3] = BitConverter.ToInt32(receiveBytes, 12);


                    SendPacket(receiveBytes);


                    for (int i = 0; i < m_requestedIDs.Count; i++)
                    {
                        if (recievePacket[0] == m_requestedIDs[i])
                        {
                            TransferPacket pack = new TransferPacket();
                            pack.object_id = recievePacket[0];
                            pack.packet_id = recievePacket[1];
                            pack.size = recievePacket[2];
                            pack.data = recievePacket[3];

                            Console.WriteLine(pack.data);

                            m_requestedObj[i].AddPacket(pack);

                            if(m_requestedObj[i].m_full)
                            {
                                m_finishedObj.Add(m_requestedObj[i]);
                                m_requestedIDs.RemoveAt(i);
                                m_requestedObj.RemoveAt(i);
                            }
                        }
                    }

                   /* Console.WriteLine("OBJID, PACKETID, SIZE, DATA: " + recievePacket[0]
                        + ", " + recievePacket[1] + " " + recievePacket[2] + " " + recievePacket[3]);

                    Console.WriteLine("This message was sent from " +
                                                RemoteIpEndPoint.Address.ToString() +
                                                " on their port number " +
                                                RemoteIpEndPoint.Port.ToString());*/
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
