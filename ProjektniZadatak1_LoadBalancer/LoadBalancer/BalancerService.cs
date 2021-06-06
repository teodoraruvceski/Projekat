using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoadBalancer
{
	/// <summary>
	/// Klasa koja vrsi servis paljenja/gasenja workera, omogucava upis i obradu podataka koji su ranije buffer-ovani
	/// </summary>
	public class BalancerService : IBalancerService
    {
		Description one;
		Description two;
		Description three;
		Description four;
		static ListDescription listDescription;
		static List<Worker> workers;

		public List<Worker> Workers { 
			get => workers;
			set { workers = value; }
		}
        public Description One { get => one; set => one = value; }
        public Description Two { get => two; set => two = value; }
        public Description Three { get => three; set => three = value; }
        public Description Four { get => four; set => four = value; }
        public static ListDescription ListDescription { get => listDescription; set => listDescription = value; }

        public BalancerService()
		{
			One = new Description(1);
			Two = new Description(2);
			Three = new Description(3);
			Four = new Description(4);

			Workers = new List<Worker>();
			Workers.Add(new Worker());
			ListDescription = new ListDescription(new List<Description>() { One, Two, Three, Four });
		}
		/// <summary>
		/// Metoda koja poziva metodu obradi od workera i radi na principu RoundRobin, pravilno rasporedjuje posao na sve workere
		/// </summary>
		[ExcludeFromCodeCoverage]
		public void StartWorkers()
		{           
            int brojWorkera = workers.Count();
			int i = 0;
			Console.WriteLine("Workers started!");
			while (true)
			{				
				if (Workers.Count() > 0)
				{
					if (i > Workers.Count() - 1)
					{
						i = 0;
					}
					workers[i].Obrada(ListDescription, i);
					foreach (Description d in ListDescription.List)
					{
						d.ListItem.Clear();
					}
					Console.WriteLine("worker " + i);
					i++;
					Thread.Sleep(5000);
				}
				else
					i = 0;
			}
		}

		/// <summary>
		/// Metoda koju poziva klijent putem proxy wcf komunikacije. Ova metoda privremeno baferuje podatke
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Write(Item item)
		{		
			Console.WriteLine("Code: " + item.Code + " Value: " + item.Value);
			if (item.Code == Code.CODE_ANALOG || item.Code == Code.CODE_DIGITAL)
			{
				if(ListDescription.List==null)
                {
					throw new NullReferenceException("Null");
				}
				foreach (Description d in ListDescription.List)
				{
					if (d.DataSet == 1)
						d.Add(item);
				}
			}
			else if (item.Code == Code.CODE_CUSTOM || item.Code == Code.CODE_LIMITSET)
			{
				if (ListDescription.List == null)
				{
					throw new NullReferenceException("Null");
				}
				foreach (Description d in ListDescription.List)
				{
					if (d.DataSet == 2)
						d.Add(item);
				}
			}
			else if (item.Code == Code.CODE_SINGLENODE || item.Code == Code.CODE_MULTIPLENODE)
			{
				if (ListDescription.List == null)
				{
					throw new NullReferenceException("Null");
				}
				foreach (Description d in ListDescription.List)
				{
					if (d.DataSet == 3)
						d.Add(item);
				}
			}
			else if (item.Code == Code.CODE_SOURCE || item.Code == Code.CODE_CONSUMER)
			{
				if (ListDescription.List == null)
				{
					throw new NullReferenceException("Null");
				}
				foreach (Description d in ListDescription.List)
				{
					if (d.DataSet == 4)
						d.Add(item);
				}
			}
			return true;
		}
		/// <summary>
		/// Metoda koja pali workera i dodaje u listu
		/// </summary>
		/// <returns></returns>
		public bool On()
		{
			if (Workers == null)
			{
				throw new NullReferenceException("Null");
			}
			Worker w = new Worker();
			Workers.Add(w);
			//log.Write(DateTime.Now, workers.Count() - 1, false);
			Console.WriteLine("Upaljen worker!");
			Console.WriteLine("Trenutan broj workera je " + workers.Count());
			return true;
		}

		/// <summary>
		/// Metoda koja gasi workera i uklanja ga iz liste.
		/// Ne obazire se tacno kog workera gasi
		/// </summary>
		public bool Off()
		{
			if(Workers==null)
            {
				throw new NullReferenceException("Null");
            }
			if (Workers.Count() > 0)
				Workers.RemoveAt(Workers.Count() - 1);
			else            
				throw new Exception("Nema workera");							
			Console.WriteLine("Ugasen worker!");
			return true;
		}
		/// <summary>
		/// Dobavlja iteme iz odredjenog vremenskog intervala za odredjenog workera
		/// </summary>
		/// <param name="workerId"></param>
		/// <param name="code"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <returns></returns>
		public List<Item> ItemsInterval(int workerId, Code code, DateTime from, DateTime to)
		{
			if(workerId<0 )
            {
				throw new ArgumentException("Id workera ne sme biti manji od nule i mora biti manji od broja aktivnih workera");
            }
			else if(code<0 || code>(Code)7)
            {
				throw new ArgumentException("Kod mora biti pozitivan i manji od 8");
            }
			else if(from>DateTime.Now  || from>to)
            {
				throw new ArgumentException("Pogresan datum.");
			}			
			Off();
			Worker w = new Worker();
			List<Item> ret = w.ItemsInterval(workerId, code, from, to);
			On();			
			return ret;
		}
		/// <summary>
		/// Pomocna metoda za korisnicki UI, da ispise koliko je aktivno workera
		/// </summary>
		/// <returns></returns>
		public int NumberOfWorkers()
		{
			if (Workers == null)
			{
				throw new NullReferenceException("Null");
			}
			return workers.Count();
		}
	}
}
