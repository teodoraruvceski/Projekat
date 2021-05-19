using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Common
{
	/// <summary>
	/// Item ce biti tip podataka koji se salje od klijenta ka serveru na obradu
	/// </summary>
	/// Objekti klase item ce se slati u smjeru klijenta ka serveru pa je i ova klasa datacontract
	[DataContract]
	public class Item
	{
		[DataMember]
		public double Value;
		public Item()
		{

		}
	}
}
