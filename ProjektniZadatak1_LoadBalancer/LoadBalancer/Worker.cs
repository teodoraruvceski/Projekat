using Common;
using LoadBalancer.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    /// <summary>
    /// Worker kao klasa koja sluzi da obradjuje podatke bufferovane u load balanceru i upisuje ih u bazu
    /// </summary>
    public class Worker
    {
        int id;
        static int count = 0;
        static DBProvider dbp=new DBProvider();

        public int Id { get => id; set => id = value; }
        public static int Count { get => count; set => count = value; }
        public static DBProvider Dbp { get => dbp; set => dbp = value; }

        public Worker()
        {
            Id = Count++;
        }
        ~Worker() { Count--; }
        /// <summary>
        /// Metoda koja poziva dodavanje entiteta u bazu
        /// </summary>
        /// <param name="ld"></param>
        /// <param name="wid"></param>
        public void Obrada(ListDescription ld, int wid)
        {
            if (wid<0)
            {
                throw new ArgumentException("Ne smije biti negativnih argumenata");
            }
            if(ld.List==null)
            {
                throw new ArgumentNullException("Null");
            }

            foreach (Description d in ld.List)
            {
                Dbp.Add(d, wid);
            }
        }
        /// <summary>
        /// Metoda za vezana za citanje upisanih podataka iz baze
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="code"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>Vraca listu podataka tipa Item za zahtijevani ID workera</returns>
        public List<Item> ItemsInterval(int workerId, Code code, DateTime from, DateTime to)
        {
            if(workerId<0 )
            {
                throw new ArgumentException("Id worker-a mora biti pozitivan i manji od broja postojecih workera.");
            }
            else if(from > to || from > DateTime.Now )
            {
                throw new ArgumentException("Pogresan format datuma.");
            }
            else if (code < 0 || code > (Code)7)
            {
                throw new ArgumentException("Kod mora biti pozitivan i manji od 7.");
            }
            List < Item > li= Dbp.GetItems(workerId, code, from, to);
            return li;
        }

    }
}
