using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	class Program
	{
		[ExcludeFromCodeCoverage]
		public static IBalancerService CreateChannel(string address)
        {
            try
            {
				//Desifinsanje parametara konekcije ka serveru koriteci interfejs IBalancerService
				ChannelFactory<IBalancerService> cf = new ChannelFactory<IBalancerService>(new NetTcpBinding(), new EndpointAddress(address));
				//Pravljenje kanala 
				IBalancerService kanal = cf.CreateChannel();
				return kanal;
			}
			catch
            {
				throw new AddressAccessDeniedException("Neuspesna konekcija na server.");
            }
        }
		[ExcludeFromCodeCoverage]
		static void Main(string[] args)
		{
			Writer w = new Writer();
			IBalancerService kanal = CreateChannel("net.tcp://localhost:4002/IBalancerService");
			// pocetak slanja w.Start(...)
			w.Start(kanal);

			Console.WriteLine("Klijent je zavrsio sa radom. Pritisni bilo sta za izlaz...");
			Console.ReadLine();
		}
	}
}
