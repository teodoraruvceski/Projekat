using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.DB
{
	public class ItemTable
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public double Value { get; set; }
		public DateTime TimeStamp { get; set; }
		public int WorkerId { get; set; }
	}
}
