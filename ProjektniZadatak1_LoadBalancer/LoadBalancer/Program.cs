using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using LoadBalancer.DB;

namespace LoadBalancer
{
    class Program
    {
		public static void Conn()
		{
			ServiceHost sh = new ServiceHost(typeof(BalancerService));
			sh.AddServiceEndpoint(typeof(IBalancerService), new NetTcpBinding(), new Uri("net.tcp://localhost:4002/IBalancerService"));
			sh.Open();
			Console.WriteLine("Servis otvoren.");

			Console.Read();
			sh.Close();
		}
		static void Main(string[] args)
        {
			{
				DBProvider p = new DBProvider();
				p.DeleteAll();
			}
			Log log = new Log(@"FILElog\writerLogServer.txt");
			Task task = Task.Factory.StartNew(() => Conn());
			BalancerService bs = new BalancerService();
			Task task2 = Task.Factory.StartNew(() => bs.StartWorkers());

			Console.WriteLine("Press any key to close application.");
			Console.Read();
			log.Write(DateTime.Now);
		}
    }
}
