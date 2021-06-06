using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using LoadBalancer.DB;

namespace LoadBalancer
{
	[ExcludeFromCodeCoverage]
	class Program
    {
		/// <summary>
		/// Uspostava konekcije
		/// </summary>
		[ExcludeFromCodeCoverage]
		public static void Conn()
		{
			ServiceHost sh = new ServiceHost(typeof(BalancerService));
			sh.AddServiceEndpoint(typeof(IBalancerService), new NetTcpBinding(), new Uri("net.tcp://localhost:4002/IBalancerService"));
			sh.Open();
			Console.WriteLine("Servis otvoren.");

			Console.Read();
			sh.Close();
		}
		[ExcludeFromCodeCoverage]
		static void Main(string[] args)
        {
			{
				//Brisanje svih podataka baze zbog testiranja
				DBProvider p = new DBProvider();
				p.DeleteAll();
			}
			Log log = new Log(@"FILElog\writerLogServer.txt");
			//Task koji obavlja konekciju
			Task task = Task.Factory.StartNew(() => Conn());
			BalancerService bs = new BalancerService();
			//Task koji pokece obradu
			Task task2 = Task.Factory.StartNew(() => bs.StartWorkers());

			Console.WriteLine("Press any key to close application.");
			Console.Read();
			//Logovanje gasenja aplikacije
			log.WriteTurnOff(DateTime.Now);
		}
    }
}
