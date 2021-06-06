using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
		public int id;
		List<Item> listItem;
		int dataSet;
		public static int count = 0;

		[ExcludeFromCodeCoverage]
		public List<Item> ListItem
		{
			get { return listItem; }
			set { listItem = value; }
		}

		public Description(int data)
		{
			if (data < 1 || data > 4)
			{
				throw new ArgumentException("Argument ne sme biti manji od 1 ili veci od 4.");
			}
			else
			{
				listItem = new List<Item>();
				id = ++count;
				this.dataSet = data;
			}
		}

		public int DataSet { get => dataSet; }
		[ExcludeFromCodeCoverage]
		public void Add(Item item)
		{
			listItem.Add(item);
		}
	}
}
