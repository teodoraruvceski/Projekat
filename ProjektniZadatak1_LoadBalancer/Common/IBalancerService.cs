using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Interfejs koji ce imlementirati serverska strana.
    /// Klijentska strana ce koristiti operacije interfejsa.
    /// </summary>
    public interface IBalancerService
    {
        bool Write();

        bool On();

        bool Off();

        List<Item> ItemsInterval();
    }
}
