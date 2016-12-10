using System;
using Nancy.Hosting.Self;
using ConvenienceSystemBackendServer;
using System.Threading;

namespace ConvenienceSystemServer
{
    class Program
    {
        static void Main(string[] args)
        {
            /***
             * Note:
             *  When using multiple network interfaces, you should not use localhost but a fully qualified ip (v4)
             * 
             ***/
            //SSL: For SSL on Windows rewrite http to https
            var uri = new Uri("http://localhost:"+ConvenienceSystemBackendServer.ConvenienceServer.getPort());
            //var uri = new Uri("http://192.168.1.39:" + ConvenienceSystemBackendServer.ConvenienceServer.getPort());


            using (var host = new NancyHost(uri))
            {
                host.Start();

                Console.WriteLine("Your application is running on " + uri);
                /*Console.WriteLine("Enter 'q' and press [Enter] to close the host.");

                while (true)
                {
                    string line = Console.ReadLine();
                    if (line == "q")
                        break;
                }*/
                while (true)
                    Thread.Sleep(50000);

                /*
                * Remark:
                *   using Sleep here because ReadLine/yield on Unix/Linux often has problems with nohup execution resulting in continously high load on the server
                */
            }
        }
    }
}
