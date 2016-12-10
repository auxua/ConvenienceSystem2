using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConvenienceSystemBackendServer;
using ConvenienceSystemDataModel;

namespace ConvenienceSystemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvenienceServer cs = new ConvenienceServer();
            //var ans = cs.GetAllUsersAsync("foo").Result;

            //var ans = cs.BuyDirectlyAsync("ZZTest-User", "Wasser", 0.10);

            var ans2 = cs.GetAccountingForUserAsync("ZZTest-User").Result;


            Console.ReadLine();

        }
    }
}
