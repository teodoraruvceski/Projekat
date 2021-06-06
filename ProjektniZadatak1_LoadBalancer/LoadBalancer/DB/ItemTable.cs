using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.DB
{
	/// <summary>
	/// Klasa kog su oblika entiteti koji se upisuju u bazu
	/// </summary>
	public class ItemTable
	{
		public ItemTable(string code, double value, DateTime timeStamp, int workerId)
		{
			try
			{
				Enum.Parse(typeof(Code), code);
			}
			catch
            {
				throw new ArgumentException("Kod nije dobar");
            }
			if (value < 0)
			{
				throw new ArgumentException("Vrednost ne sme biti negativna");
			}
			else if(timeStamp>DateTime.Now)
            {
				throw new ArgumentException("Datum nije validan");
			}
			else if(workerId<0)
            {
				throw new ArgumentException("Id workera nije dobar");
			}				
			Code = code;
            Value = value;
            TimeStamp = timeStamp;
            WorkerId = workerId;
        }
		[ExcludeFromCodeCoverage]
		public int Id { get; set; }
		public string Code { get; set; }
		public double Value { get; set; }
		public DateTime TimeStamp { get; set; }
		public int WorkerId { get; set; }
	}
}
