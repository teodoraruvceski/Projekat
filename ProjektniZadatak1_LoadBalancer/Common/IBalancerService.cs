using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	/// <summary>
	/// Interfejs koji ce imlementirati serverska strana.
	/// Klijentska strana ce koristiti operacije interfejsa.
	/// </summary>
	/// Dodat operation contract i operation contract jer ce ova klasa i njene metoe koristiti za wcf komunikaciju
	[ServiceContract]
	public interface IBalancerService
    {
		[OperationContract]
		bool Write();

		[OperationContract]
		bool On();

		[OperationContract]
		bool Off();

		[OperationContract]
		List<Item> ItemsInterval();
    }
}
