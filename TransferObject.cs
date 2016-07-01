using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confetti
{

    class TransferPacket
    {
        public int data         = 0;
        public int object_id    = 0;
        public int packet_id    = 0;
        public int size         = 0;
    }

    class TransferObject
    {
        private int[]   m_transferArray         = new int[5] { 1, 2, 3, 4, 5 };
        private bool[]  m_transferArrayExists   = new bool[5] { true, true, true, true, true };
        private int     m_id                    = 8;

        public TransferObject( int _id)
        {
            m_id = _id;
        }

        public TransferPacket GetPacket()
        {
            TransferPacket obj = new TransferPacket();

            for (int i = 0; i < m_transferArray.Count(); i++)
            {
                if(m_transferArrayExists[i] == true)
                {
                    obj.data        = m_transferArray[i];
                    obj.object_id   = m_id;
                    obj.packet_id   = i;
                    obj.size        = 5;

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
        private List<int>   m_recievedData      = new List<int>();
        private List<int>   m_recievedIndices   = new List<int>();
        private int         m_size = 0;
        private int         m_id;


        public bool m_full = false;

        public RecieveObject( int _id)
        {
            m_id = _id;
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
            }
        }
    }


    }
