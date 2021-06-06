using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Diagnostics.CodeAnalysis;

namespace Common
{
	/// <summary>
	/// Item ce biti tip podataka koji se salje od klijenta ka serveru na obradu
	/// </summary>
	/// Stuktura item ce se slati u smjeru klijenta ka serveru pa je i ova klasa datacontract
	[DataContract]
	public struct Item
	{
		[DataMember]
		public Code Code;
		[DataMember]
		public double Value;

		public Item(Code c, double v)
		{
			if(c>=0 && c<(Code)8 && v>=0)
			{
				Code = c;
				Value = v;
			}
			else if(c<0 || c<0)
			{
				throw new ArgumentException("Argumenti ne smeju biti negativni.");
			}
			else
			{
				throw new ArgumentException("Kod mora biti manji od 8.");
			}			
		}
		[ExcludeFromCodeCoverage]
		public override string ToString()
		{
            return Code + " " + Value;
		}
	}
}
