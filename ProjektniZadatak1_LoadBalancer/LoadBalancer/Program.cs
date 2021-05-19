using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    class Program
    {
		public static void Conn()
		{
			ServiceHost sh = new ServiceHost(typeof(BalancerService));
			sh.AddServiceEndpoint(typeof(Common.IBalancerService), new NetTcpBinding(), new Uri("net.tcp://localhost:4002/IBalancerService"));
			sh.Open();
			Console.WriteLine("Servis otvoren.");

			Console.Read();
			sh.Close();
		}
		static void Main(string[] args)
        {
        }
    }
}
