using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
	/// <summary>
	/// Struktura sa 4 Description strukture jer imamo 4 razlicita DataSet-a
	/// </summary>
	public struct ListDescription
	{
		List<Description> list;
		public List<Description> List { get => list; set { list = value; } }
		public ListDescription(List<Description> l)
		{
			if (l == null)
			{
				throw new ArgumentNullException("Argument ne sme biti null.");
			}
			list = l;
		}
	}
}
