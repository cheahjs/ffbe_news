using System;
using System.Threading;

namespace FFBENews
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Networking.SendRequest(new Network.Japan.NoticeUpdateRequest(false));
                    Networking.SendRequest(new Network.Japan.NoticeUpdateRequest(true));
                    Networking.SendRequestGlobal(new Network.Global.NoticeUpdateRequest(false));
                    Networking.SendRequestGlobal(new Network.Global.NoticeUpdateRequest(true));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                Thread.Sleep(1*60*1000);
            }
        }
    }
}
