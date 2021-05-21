using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{

	/// <summary>
	/// Klasa koja salje serveru podatke i trazi njihovu obradu.
	/// Ona izvrsava klijentske narebe
	/// </summary>
	public class Writer
	{
		public Writer()
		{

		}
		/// <summary>
		/// Metoda za slanje svako dvije sekune proizvoljnih vrijednosti Item stukture
		/// </summary>
		/// <param name="kanal">Kao parametar koji se salje kanalom</param>
		public void Start(IBalancerService kanal)
		{

			int a, b;
			Item item;
			Random r = new Random();
			Random rr = new Random();
			double v = r.Next(1, 1000);
			int c = rr.Next(0, 7);

			

			while (true)
			{
				//Pritisak na bilo koje ugme prekida slanje
				if (Console.KeyAvailable)
				{
					if (Console.ReadKey().Key == ConsoleKey.Enter)
							break;
				}

				Thread.Sleep(2000);
				item = new Item((Code)c, v);

				kanal.Write(item);//Slanje item-a
				v = r.NextDouble() * (1000.0 - 1.0) + 1.0;
				c = rr.Next(0, 7);
			}		
		}
	}
}
