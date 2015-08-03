using System;
using Nancy.Hosting.Self;
using ConvenienceSystemBackendServer;

namespace ConvenienceSystemServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri("http://localhost:"+ConvenienceSystemBackendServer.ConvenienceServer.getPort());

            using (var host = new NancyHost(uri))
            {
                host.Start();

                Console.WriteLine("Your application is running on " + uri);
                Console.WriteLine("Press any [Enter] to close the host.");
                Console.ReadLine();
            }
        }
    }
}
