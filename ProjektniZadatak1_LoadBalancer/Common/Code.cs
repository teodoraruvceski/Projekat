using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	/// <summary>
 /// Enumeracija za tipove kodova unutar Item strukture. Po dva u paru za jedan DataSet
 /// </summary>

	public enum Code
	{
		[EnumMember] CODE_ANALOG,
		[EnumMember] CODE_DIGITAL,
		[EnumMember] CODE_CUSTOM,
		[EnumMember] CODE_LIMITSET,
		[EnumMember] CODE_SINGLENODE,
		[EnumMember] CODE_MULTIPLENODE,
		[EnumMember] CODE_CONSUMER,
		[EnumMember] CODE_SOURCE
	}
}
