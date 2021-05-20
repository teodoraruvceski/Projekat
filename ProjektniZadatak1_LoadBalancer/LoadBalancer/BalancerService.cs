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
    }
}
