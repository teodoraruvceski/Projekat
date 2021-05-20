using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

	public class BalancerService
    {
		Description one;
		Description two;
		Description three;
		Description four;
		static ListDescription listDescription;

		public BalancerService()
		{

			one = new Description(1);
			two = new Description(2);
			three = new Description(3);
			four = new Description(4);




			listDescription = new ListDescription(new List<Description>() { one, two, three, four });

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
	}
}
