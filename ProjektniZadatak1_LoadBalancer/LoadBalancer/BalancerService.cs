﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoadBalancer
{
	/// <summary>
	/// Struktura u koju se smijestaju item-i jednog DataSet-a
	/// </summary>
	public struct Description
	{
		int id;
		List<Item> listItem;
		int dataSet;
		static int count = 0;
		public List<Item> ListItem
		{
			get { return listItem; }
		}

		public Description(int data)
		{
			listItem = new List<Item>();
			id = ++count;
			this.dataSet = data;

		}
		public int DataSet { get => dataSet; }
		public void Add(Item item)
		{
			listItem.Add(item);
		}
	}
	/// <summary>
	/// Struktura sa 4 Description strukture jer imamo 4 razlicita DataSet-a
	/// </summary>
	public struct ListDescription
	{
		List<Description> list;
		public List<Description> List { get => list; set => list = value; }
		public ListDescription(List<Description> l)
		{
			list = l;
		}
	}

	public class BalancerService : IBalancerService
    {
		Description one;
		Description two;
		Description three;
		Description four;
		static ListDescription listDescription;
		static List<Worker> workers;

		public List<Worker> Workers { get => workers; set => workers = value; }

		public BalancerService()
		{

			one = new Description(1);
			two = new Description(2);
			three = new Description(3);
			four = new Description(4);



			Workers = new List<Worker>();
			Workers.Add(new Worker());
			listDescription = new ListDescription(new List<Description>() { one, two, three, four });

		}
		/// <summary>
		/// Metoda koja poziva metodu obradi od workera i radi na principu RoundRobin, pravilno rasporedjuje posao na sve workere
		/// </summary>
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
					// i. worker obradjuje trenutni buffer
					workers[i].Obrada(listDescription, i);
					foreach (Description d in listDescription.List)
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

		public bool Write(Item item)
		{
			Console.WriteLine("Code: " + item.Code + " Value: " + item.Value);
			if (item.Code == Code.CODE_ANALOG || item.Code == Code.CODE_DIGITAL)
			{
				foreach (Description d in listDescription.List)
				{
					if (d.DataSet == 1)/////
						d.Add(item);
				}
			}

			else if (item.Code == Code.CODE_CUSTOM || item.Code == Code.CODE_LIMITSET)
			{
				foreach (Description d in listDescription.List)
				{
					if (d.DataSet == 2)
						d.Add(item);
				}
			}
			else if (item.Code == Code.CODE_SINGLENODE || item.Code == Code.CODE_MULTIPLENODE)
			{
				foreach (Description d in listDescription.List)
				{
					if (d.DataSet == 3)
						d.Add(item);
				}
			}
			else if (item.Code == Code.CODE_SOURCE || item.Code == Code.CODE_CONSUMER)
			{
				foreach (Description d in listDescription.List)
				{
					if (d.DataSet == 4)
						d.Add(item);
				}
			}


			return true;
		}

		public bool On()
		{
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
			if (Workers.Count() > 0)
				Workers.RemoveAt(Workers.Count() - 1);
			else
				return false;
			Console.WriteLine("Ugasen worker!");
			return true;

		}

		public List<Item> ItemsInterval(int workerId, Code code, DateTime from, DateTime to)
		{
			Off();
			Worker w = new Worker();
			List<Item> ret = w.ItemsInterval(workerId, code, from, to);
			On();
			return ret;
		}

		public int NumberOfWorkers()
		{
			throw new NotImplementedException();
		}
	}
}
