using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confetti
{

    class TransferPacket
    {
        public byte data        = 0; 
        public byte data1       = 0;//Padding
        public byte data2       = 0;//Padding
        public byte data3       = 0;//Padding
        public int object_id    = 0;
        public int packet_id    = 0;
        public int size         = 0;
    }

    class TransferObject
    {
        private byte[]   m_transferArray;
        private bool[]  m_transferArrayExists;
        private int     m_id                    = 8;

        public TransferObject( int _id, String _name )
        {
            m_id = _id;

            LoadDataToTransfer(_name);
        }

        public bool LoadDataToTransfer(String _filename)
        {
            byte[] data = File.ReadAllBytes(_filename);

            m_transferArray = data;
            m_transferArrayExists = new bool [data.Count()];
            for(int i = 0; i <  m_transferArrayExists.Count(); i++)
            {
                m_transferArrayExists[i] = true;
            }

            return true;
        }

        public TransferPacket GetPacket()
        {
            TransferPacket obj = new TransferPacket();

            //This is slow. But it is just to test concept. Lets not worry about speed at the moment.
            for (int i = 0; i < m_transferArray.Count(); i++)
            {
                if(m_transferArrayExists[i] == true)
                {
                    obj.data        = m_transferArray[i];
                    obj.object_id   = m_id;
                    obj.packet_id   = i;
                    obj.size        = m_transferArrayExists.Count();

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
        private List<int>   m_recievedIndices   = new List<int>();
        private int         m_size = 0;
        private int         m_id;


        public bool m_full = false;

        public RecieveObject( int _id)
        {
            m_id = _id;
        }

        public bool WriteFile()
        {
            String name = "NewFile.txt";
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
                    m_recievedData.Add(_in.data);
                    m_recievedIndices.Add(_in.packet_id);
                }
            }

            if (m_recievedData.Count == m_size)
            {
                m_full = true;
                WriteFile();
            }
        }
    }


    }
