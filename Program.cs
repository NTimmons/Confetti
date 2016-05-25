using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tinsel
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller MainController = new Controller();

            MainController.AddNode("127.0.0.1");
            MainController.InitClient(11001);

            TransferObject TransObj = new TransferObject();

            MainController.AddRequest(8);
            MainController.StartBouncing();

            Thread.Sleep(100);
            TransferPacket pack = TransObj.GetPacket();
            MainController.SendPacket(pack);

            Thread.Sleep(1000);
            pack = TransObj.GetPacket();
            MainController.SendPacket(pack);

            Thread.Sleep(1000);
            pack = TransObj.GetPacket();
            MainController.SendPacket(pack);

            Thread.Sleep(1000);
            pack = TransObj.GetPacket();
            MainController.SendPacket(pack);

            Thread.Sleep(1000);
            pack = TransObj.GetPacket();
            MainController.SendPacket(pack);

            while (true)
            {
                Thread.Sleep(1000);
                Debug.Write(".");
            }

            MainController.StopBouncing();
        }
    }
}
