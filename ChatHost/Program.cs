using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF_Chat;

namespace ChatHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(var host = new ServiceHost(typeof(ServiceChat)))
            {

                host.Open();
                Console.WriteLine("Host start");
                Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}
