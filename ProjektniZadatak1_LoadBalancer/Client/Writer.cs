using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
		private Log log;

		public Writer()
		{
			Log = new Log(@"FILElog\writerLog.txt");        
		}
		public Log Log { get => log; set => log = value; }

		/// <summary>
		/// Menu koji ispisuje moguce komande korisnika
		/// </summary>
		[ExcludeFromCodeCoverage]
		public int Menu()
		{
			Console.WriteLine("MENU:");
			Console.WriteLine("1. TURN OFF worker");
			Console.WriteLine("2. TURN ON worker");
			Console.WriteLine("3. Write Item");
			Console.WriteLine("4. List items from interval");
			Console.WriteLine("5. Exit program");
			int i;
			try
			{
				i = int.Parse(Console.ReadLine());
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				i = -1;
			}
			return i;
		}
		/// <summary>
		/// Menu2 koji nudi moguce kodove za rucno slanje podataka ka Balancer servisu
		/// </summary>
		[ExcludeFromCodeCoverage]
		public int Menu2()
		{
			int a;
			Console.WriteLine("Unesite CODE:");
			Console.WriteLine("CODE_ANALOG -> 1");
			Console.WriteLine("CODE_DIGITAL -> 2");
			Console.WriteLine("CODE_CUSTOM -> 3");
			Console.WriteLine("CODE_LIMITSET -> 4");
			Console.WriteLine("CODE_SINGLENODE -> 5");
			Console.WriteLine("CODE_MULTIPLENODE -> 6");
			Console.WriteLine("CODE_CONSUMER -> 7");
			Console.WriteLine("CODE_SOURCE -> 8");
			while (true)
			{
				if (int.TryParse(Console.ReadLine(), out a))
					if (a < 9 && a > 0)
					{
						break;
					}
			}
			return a-1;
		}
		/// <summary>
		/// Menu3  koji nudi unos value za rucno slanje podataka servisu
		/// </summary>
		[ExcludeFromCodeCoverage]
		public int Menu3()
		{
			int b;
			while (true)
			{
				Console.WriteLine("Unesite VALUE:");
				if (int.TryParse(Console.ReadLine(), out b))
					if (b > -1)
						break;
			}
			return b;
		}
		/// <summary>
		/// Metoda za slanje svako dvije sekune proizvoljnih vrijednosti Item stukture
		/// </summary>
		/// <param name="kanal">Kao parametar koji se salje kanalom</param>
		[ExcludeFromCodeCoverage]
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
				Console.WriteLine("Ako zelite da pauzirate rad writera pritisnite [ENTER].");
				while (true)
				{
					//Pritisak na bilo koje dugme prekida slanje
					if (Console.KeyAvailable)
					{
						if (Console.ReadKey().Key == ConsoleKey.Enter)
								break;
					}
					Thread.Sleep(2000);
					item = new Item((Code)c, v);

					kanal.Write(item);//Slanje item-a
					Log.Write((Code)c, v, DateTime.Now); //Logovanje upisa
					v = r.NextDouble() * (1000.0 - 1.0) + 1.0;
					c = rr.Next(0, 7);
				}
				Console.Clear();

				switch (Menu())
				{
					case 1:
                        {
							try
                            {
								kanal.Off();//opcija 1 gasi workera
							}
							catch(NullReferenceException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
							break;
						}
					case 2:
						{
							try
							{
								kanal.On();//opcija 2 upali workera
							}
							catch (NullReferenceException ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						}
					case 3:
						a = Menu2();//rucno slanje podataka ka LoadBalanceru
						b = Menu3();
						try
                        {
							kanal.Write(new Item((Code)a, b));
						}
						catch(NullReferenceException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
						break;
					case 4:
						a = Menu2();//izlistavanje podataka iz baze podataka za dati kod i vremenski interval
						while (true)
						{
							int br = kanal.NumberOfWorkers();
							Console.WriteLine($"Unesite workerId [0-{br - 1}]:");
							if (int.TryParse(Console.ReadLine(), out b))
								//if (b > -1 && b < br - 1)
									break;
						}
						Console.WriteLine("Unesite vreme od [format -> 2009 - 12 - 22 14:40:52]:");
						string s = Console.ReadLine();
						DateTime myDate;
						DateTime myDate2;
						try
                        {
							 myDate = DateTime.ParseExact(s, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
							Console.WriteLine("Unesite vreme do [format -> 2009 - 12 - 22 14:40:52]:");
							string ss = Console.ReadLine();
							 myDate2 = DateTime.ParseExact(ss, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
							List<Item> lista = kanal.ItemsInterval(b, (Code)a, myDate, myDate2);
							log.Read(myDate, myDate2, DateTime.Now, b,(Code)a);
							Console.WriteLine("RESULT: ");
							foreach (Item i in lista)
								Console.WriteLine(i);
							break;
						}
					    catch(Exception e)
                        {
							Console.WriteLine(e.Message);
							break;
                        }
						
					case 5: //odustajanje od bilo koje akcije i nakon toga program se gasi
						Log.WriteTurnOff(DateTime.Now);//biljezenje gasenja
						return;
					default:
						Console.WriteLine("Nepostojeca komanda...");
						break;
				}
			}
		}
	}
}
