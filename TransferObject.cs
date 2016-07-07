using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confetti
{

    internal unsafe struct Packet
    {
        public fixed byte m_bytes[Globals.bytesPerPacket];
    }

    internal unsafe struct TransferPacket
    {
        public fixed byte data[Globals.bytesPerPacket];
        public int object_id;
        public int packet_id;
        public int size;
    }

    class TransferObject
    {
        private byte[]  m_transferArray;
        private bool[]  m_transferArrayExists;
        private List<Packet> m_packetList = new List<Packet>();
        private int     m_id                    = 8;
        private int m_size = 0;

        public TransferObject( int _id, String _name )
        {
            m_id = _id;

            LoadDataToTransfer(_name);
        }

        public bool LoadDataToTransfer(String _filename)
        {
            byte[] data = File.ReadAllBytes(_filename);

            m_transferArray = data;
            m_size = data.Count();

            //Build packet list.
            //------------------
            for (int i = 0; i < data.Count(); i+= Globals.bytesPerPacket)
            {
                unsafe
                {
                    Packet newPacket = new Packet();
                    for(int j = 0; j < Globals.bytesPerPacket; j++)
                    {
                        if( (i + j) >= data.Count())
                        {
                            newPacket.m_bytes[j] = 0;
                        }
                        else
                        {
                            newPacket.m_bytes[j] = data[i + j];
                        }
                        
                    }

                    m_packetList.Add(newPacket);
                }
            }

            m_transferArrayExists = new bool[m_packetList.Count()];
            for (int i = 0; i < m_transferArrayExists.Count(); i++)
            {
                m_transferArrayExists[i] = true;
            }

            return true;
        }

        public TransferPacket GetPacket()
        {
            TransferPacket obj = new TransferPacket();

            //This is slow. But it is just to test concept. Lets not worry about speed at the moment.
            for (int i = 0; i < m_transferArrayExists.Count(); i++)
            {
                if(m_transferArrayExists[i] == true)
                {
                    unsafe
                    {
                        Packet p = m_packetList[i];

                        for (int j = 0; j < Globals.bytesPerPacket; j++)
                        {
                            
                            obj.data[j] = p.m_bytes[j];
                        }
                        obj.object_id = m_id;
                        obj.packet_id = i;
                        obj.size = m_size;
                    }


                    m_transferArray[i]      = 0;
                    m_transferArrayExists[i]= false;
                    break;
                }
            }
            return obj;
        }
    }

    class RecieveObject
    {
        private List<byte>  m_recievedData      = new List<byte>();
        private List<Packet> m_packetList       = new List<Packet>();
        private List<int>   m_recievedIndices   = new List<int>();
        private int         m_size = 0;
        private int         m_id;

        Stopwatch stopWatch = new Stopwatch();


        public bool m_full = false;

        public RecieveObject( int _id)
        {
            m_id = _id;
        }

        public void CopyAllPacketsToByteArray()
        {
            int counter = 0;

            for(int i = 0; i < m_packetList.Count; i++)
            {
                Packet p = m_packetList[i];
                unsafe
                {
                    for (int j = 0; j < Globals.bytesPerPacket; j++)
                    {

                        m_recievedData.Add(p.m_bytes[j]);

                        counter++;
                        if (counter > m_size)
                        {
                            return;
                        }
                    }
                }
            }
        }

        public bool WriteFile()
        {
            String name = "NewFile.txt";

            CopyAllPacketsToByteArray();

            File.WriteAllBytes(name, m_recievedData.ToArray() );

            Debug.WriteLine("File complete and written.");

            return true;
        }

        public void AddPacket(TransferPacket _in)
        {
            if(m_size < _in.size)
            {
                m_size = _in.size;
            }

            if (_in.object_id == m_id)
            {
                if(!m_recievedIndices.Contains(_in.packet_id))
                {
                    if(m_packetList.Count == 0)
                    {
                        stopWatch.Start();
                    }

                    unsafe
                    {
                        Packet pack = new Packet();
                        for (int j = 0; j < Globals.bytesPerPacket; j++)
                        {
                            pack.m_bytes[j] = _in.data[j];
                        }

                        m_packetList.Add(pack);
                        m_recievedIndices.Add(_in.packet_id);
                    }
                    

                    Console.WriteLine("Complete: " + ((float)m_packetList.Count * Globals.bytesPerPacket) / (float)m_size + "%");
                }
            }

            if ( (m_packetList.Count* Globals.bytesPerPacket) >= m_size)
            {
                m_full = true;
                WriteFile();
                stopWatch.Stop();
                Console.WriteLine("Complete: " + "100%");

                TimeSpan ts = stopWatch.Elapsed;
                double ms = ts.TotalMilliseconds;
                double s = ts.TotalSeconds;
                Console.WriteLine("Speed: " + ((float)m_size / 1024.0f) / s + " kb/s.");

            }
        }
    }


    }
