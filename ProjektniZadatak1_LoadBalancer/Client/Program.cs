using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Writer w = new Writer();
			//Desifinsanje parametara konekcije ka serveru koriteci interfejs IBalancerService
			ChannelFactory<IBalancerService> cf = new ChannelFactory<IBalancerService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:4002/IBalancerService"));
			//Pravljenje kanala i pocetak slanja w.Start(...)
			IBalancerService kanal = cf.CreateChannel();
			w.Start(kanal);


			Console.WriteLine("Klijent je zavrsio sa radom. Pritisni bilo sta za izlaz...");
			Console.ReadLine();
		}
	}
}
