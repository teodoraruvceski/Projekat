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
        static DBProvider dbp = new DBProvider();

        public int Id { get => id; set => id = value; }
        public static int Count { get => count; set => count = value; }

        public Worker()
        {
            Id = ++Count;


        }
        ~Worker() { Count--; }

        public void Obrada(ListDescription ld, int wid)
        {

            foreach (Description d in ld.List)
            {
                dbp.Add(d, wid);
            }

        }
        public List<Item> ItemsInterval(int workerId, Code code, DateTime from, DateTime to)
        {
            return dbp.GetItems(workerId, code, from, to);
        }

    }
}
