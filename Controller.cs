using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace Confetti
{



    class Controller
    {


        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        private static unsafe extern void CopyMemory(void* dest, void* src, int count);

        private List<string>        m_knownNodes    = new List<string>();

        private List<int>           m_requestedIDs  = new List<int>();
        private List<RecieveObject> m_requestedObj  = new List<RecieveObject>();
        private List<RecieveObject> m_finishedObj   = new List<RecieveObject>();
        
        private UdpClient           udpClient;
        private int                 m_srcPort;
        private int                 m_dstPort;
        private bool                m_bouncing      = false;
        private Thread              m_workerThread;

        public void AddRequest( int id)
        {
            m_requestedIDs.Add(id);
            m_requestedObj.Add(new RecieveObject(id));
        }

        public void AddNode(string _ip)
        {
            m_knownNodes.Add(_ip);
        }

        public void InitClient(int _srcPort, int _dstPort)
        {
            m_srcPort = _srcPort;
            m_dstPort = _dstPort;
            udpClient = new UdpClient();
        }

        public void SendUdp(string dstIp, int dstPort, byte[] data)
        {
            udpClient.Send(data, data.Length, dstIp, dstPort);
        }

        //Send packet
        internal unsafe struct outPacket
        {
           public int obj_id;
           public int packet_id;
           public int size;
           public fixed byte fixedBuffer[Globals.bytesPerPacket];
        };

        public bool SendPacket(TransferPacket _packet)
        {
            bool anySent = false;


            outPacket dat = new outPacket();
            outPacket[] datArray = { dat };

            unsafe
            {
                dat.obj_id = _packet.object_id;
                dat.packet_id = _packet.packet_id;
                dat.size = _packet.size;

                for (int j = 0; j < Globals.bytesPerPacket; j++)
                {
                    dat.fixedBuffer[j] = _packet.data[j];
                }

                byte[] bytePacket = new byte[sizeof(outPacket)];

                unsafe
                {
                        fixed (void* s = &bytePacket[0])
                        {
                            CopyMemory(s, &dat, sizeof(outPacket));
                        }
                }

                //Buffer.BlockCopy(datArray, 0, bytePacket, 0, bytePacket.Length);

                for (int i = 0; i < m_knownNodes.Count(); i++)
                {
                    SendUdp(m_knownNodes[i], 11000, bytePacket);
                }
            }

            return anySent;
        }

        public bool SendPacket(byte[] _packet)
        {
            bool anySent = false;
            for (int i = 0; i < m_knownNodes.Count(); i++)
            {
                SendUdp(m_knownNodes[i], 11000, _packet);
            }

            return anySent;
        }

        public void StartBouncing()
        {
            m_bouncing = true;
            m_workerThread = new Thread(BounceFunction);

            m_workerThread.Start();
            while (!m_workerThread.IsAlive);
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

            //Creates an IPEndPoint to record the IP Address and port number of the sender. 
            // The IPEndPoint will allow you to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            while (m_bouncing)
            {
                try
                {
                    // Blocks until a message returns on this socket from a remote host.
                    Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                    string returnData = Encoding.ASCII.GetString(receiveBytes);

                    int[] recievePacket = new int[3];

                    recievePacket[0] = BitConverter.ToInt32(receiveBytes, 0);
                    recievePacket[1] = BitConverter.ToInt32(receiveBytes, 4);
                    recievePacket[2] = BitConverter.ToInt32(receiveBytes, 8);

                    byte[] packetData = new byte[Globals.bytesPerPacket];
                    for(int i = 0; i < Globals.bytesPerPacket; i++)
                    {
                        packetData[i] = receiveBytes[i + 12];
                    }

                    bool collected = false;

                    for (int i = 0; i < m_requestedIDs.Count; i++)
                    {
                        if (recievePacket[0] == m_requestedIDs[i])
                        {
                            collected = true;

                            TransferPacket pack = new TransferPacket();

                            unsafe
                            {
                                pack.object_id = recievePacket[0];
                                pack.packet_id = recievePacket[1];
                                pack.size = recievePacket[2];
                                for (int k = 0; k < Globals.bytesPerPacket; k++)
                                {
                                    pack.data[k] = packetData[k];
                                }
                            }

                            m_requestedObj[i].AddPacket(pack);

                            if(m_requestedObj[i].m_full)
                            {
                                m_finishedObj.Add(m_requestedObj[i]);
                                m_requestedIDs.RemoveAt(i);
                                m_requestedObj.RemoveAt(i);
                            }
                        }
                    }

                    if(!collected)
                    {
                        SendPacket(receiveBytes);
                    }

                    //Console.WriteLine(RemoteIpEndPoint.Address.ToString() + " : OBJID, PACKETID, SIZE, DATA: " + recievePacket[0]
                    //    + ", " + recievePacket[1] + " " + recievePacket[2] + " " + recievePacket[3]);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
