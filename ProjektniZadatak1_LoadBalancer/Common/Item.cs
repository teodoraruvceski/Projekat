using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Common
{
	/// <summary>
	/// Enumeracija za tipove kodova unutar Item strukture. Po dva u paru za jedan DataSet
	/// </summary>
	public enum Code
	{
		[EnumMember] CODE_ANALOG, [EnumMember] CODE_DIGITAL, [EnumMember] CODE_CUSTOM, [EnumMember] CODE_LIMITSET, [EnumMember] CODE_SINGLENODE,
		[EnumMember] CODE_MULTIPLENODE, [EnumMember] CODE_CONSUMER, [EnumMember] CODE_SOURCE
	}
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
			Code = c;
			Value = v;
		}
		public override string ToString()
		{
			return Code + " " + Value;
		}
	}
}
