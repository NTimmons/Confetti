using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Confetti
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Do setup.
            StartupForm introForm = new StartupForm();
            introForm.ShowDialog();


            Controller MainController = new Controller();

            for(int i = 0; i < introForm.m_nodes.Count(); i++ )
            {
                MainController.AddNode(introForm.m_nodes[i]);
            }
            
            // Setup the controller for sending and recieving packets.
            MainController.InitClient(introForm.m_sourcePort, introForm.m_destinationPort);

            TransferObject TransObj = new TransferObject(introForm.m_outgoingID, introForm.m_filename);

            // We are looking to find any floating packets with the specific ID.
            MainController.AddRequest(introForm.m_requestID);

            // Start bouncing like Delay-line memory.
            MainController.StartBouncing();

            //Loop until all data is in the line.
            bool packetsLeft = true;
            while (packetsLeft)
            {
                Thread.Sleep(100);
                TransferPacket pack = TransObj.GetPacket();

                if (pack.size > 0)
                    MainController.SendPacket(pack);
                else
                    packetsLeft = false;
            }
                
            //Just keep running and bouncing data.
            while (true)
            {
                Thread.Sleep(1000);
                Debug.Write(".");
            }

            MainController.StopBouncing();
        }
    }
}
