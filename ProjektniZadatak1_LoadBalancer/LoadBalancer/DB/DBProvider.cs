using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.DB
{
	/// <summary>
	/// Klasa koja implementira metode za upis u bazu podataka 
	/// </summary>
	public class DBProvider
	{
		Log log;

		public Log Log { get => log; set => log = value; }

		public DBProvider()
		{
			
		}

		//Metoda za dodavanje entiteta u bazu, tabelu
		public void Add(Description d, int wid)
		{
			string ss;
			switch (d.DataSet)
			{
				case 1:
					using (var db = new ProjectDBContext())
					{

						foreach (Item i in d.ListItem)
						{
							if (i.Code == Code.CODE_DIGITAL)
							{
								ss = i.Code.ToString();
								db.One.Add(new ItemTable() { WorkerId = wid, Code = ss, Value = i.Value, TimeStamp = DateTime.Now });
								/////
								db.SaveChanges();
								continue;
							}

							var query = //VAS KOD JE VRACEN napustam
								 (from o in db.One
								  where o.Code == i.Code.ToString()
								  select o).ToList<ItemTable>();


							bool b = false;


							foreach (ItemTable ii in query)
							{
								if (Math.Abs(ii.Value - i.Value) < ii.Value * 0.02)
								{
									b = true;
									break;
								}
							}
							if (!b)
							{
								ss = i.Code.ToString();
								db.One.Add(new ItemTable() { Code = ss, Value = i.Value, WorkerId = wid, TimeStamp = DateTime.Now });
								/////
								db.SaveChanges();
							}
						}
					}
					break;
				case 2:
					using (var db2 = new ProjectDBContext2())
					{
						foreach (Item i in d.ListItem)
						{
							var query =
								(from o in db2.Two
								 where o.Code == i.Code.ToString()
								 select o).ToList<ItemTable>();
							bool b = false;
							Console.WriteLine(query.Count()); //dodao da vidim vrati li entitete

							foreach (ItemTable ii in query)
							{
								if (Math.Abs(ii.Value - i.Value) < ii.Value * 0.02)
								{
									b = true;
									break;
								}
							}
							if (!b)
							{
								ss = i.Code.ToString();
								db2.Two.Add(new ItemTable() { Code = ss, Value = i.Value, WorkerId = wid, TimeStamp = DateTime.Now });
								/////
								db2.SaveChanges();
							}
						}
					}
					break;
				case 3:
					using (var db3 = new ProjectDBContext3())
					{
						foreach (Item i in d.ListItem)
						{

							var query = from o in db3.Three
										where o.Code == i.Code.ToString()
										select o;
							bool b = false;
							foreach (ItemTable ii in query)
							{
								if (Math.Abs(ii.Value - i.Value) < ii.Value * 0.02)
								{
									b = true;
									break;
								}
							}
							if (!b)
							{
								ss = i.Code.ToString();
								db3.Three.Add(new ItemTable() { Code = ss, Value = i.Value, WorkerId = wid, TimeStamp = DateTime.Now });
								/////
								db3.SaveChanges();
							}
						}
					}
					break;
				case 4:
					using (var db4 = new ProjectDBContext4())
					{
						foreach (Item i in d.ListItem)
						{
							var query = from o in db4.Four
										where o.Code == i.Code.ToString()
										select o;
							bool b = false;
							foreach (ItemTable ii in query)
							{
								if (Math.Abs(ii.Value - i.Value) < ii.Value * 0.02)
								{
									b = true;
									break;
								}
							}
							if (!b)
							{
								ss = i.Code.ToString();
								db4.Four.Add(new ItemTable() { Code = ss, Value = i.Value, WorkerId = wid, TimeStamp = DateTime.Now });
								/////
								db4.SaveChanges();

							}
						}
					}
					break;
				default: break;
			}
		}
		//Metoda za dobijanje entiteta koj isu vec upisani u bazu
		public List<Item> GetItems(int workerId, Code code, DateTime fr, DateTime to)
		{
			List<Item> ret = new List<Item>();
			string ss;
			if (code == Code.CODE_ANALOG || code == Code.CODE_DIGITAL)
			{
				using (var db = new ProjectDBContext())
				{
					ss = code.ToString();
					var query = from o in db.One
								where o.Code == ss & o.WorkerId == workerId & o.TimeStamp < to & o.TimeStamp > fr
								select o;
					foreach (ItemTable it in query)
					{
						Code ct;
						Code.TryParse(it.Code, out ct);
						ret.Add(new Item() { Value = it.Value, Code = ct });
					}
				}
			}
			else if (code == Code.CODE_CUSTOM || code == Code.CODE_LIMITSET)
			{
				using (var db2 = new ProjectDBContext2())
				{
					ss = code.ToString();
					var query = from o in db2.Two
								where o.Code == ss && o.WorkerId == workerId && o.TimeStamp < to && o.TimeStamp > fr
								select o;
					foreach (ItemTable it in query)
					{
						Code ct;
						Code.TryParse(it.Code, out ct);
						ret.Add(new Item() { Value = it.Value, Code = ct });
					}
				}
			}
			else if (code == Code.CODE_SINGLENODE || code == Code.CODE_MULTIPLENODE)
			{
				using (var db3 = new ProjectDBContext3())
				{
					ss = code.ToString();
					var query = from o in db3.Three
								where o.Code == ss && o.WorkerId == workerId && o.TimeStamp < to && o.TimeStamp > fr
								select o;
					foreach (ItemTable it in query)
					{
						Code ct;
						Code.TryParse(it.Code, out ct);
						ret.Add(new Item() { Value = it.Value, Code = ct });
					}
				}
			}
			else if (code == Code.CODE_CONSUMER || code == Code.CODE_SOURCE)
			{
				using (var db4 = new ProjectDBContext4())
				{
					ss = code.ToString();
					var query = from o in db4.Four
								where o.Code == ss && o.WorkerId == workerId && o.TimeStamp < to && o.TimeStamp > fr
								select o;
					foreach (ItemTable it in query)
					{
						Code ct;
						Code.TryParse(it.Code, out ct);
						ret.Add(new Item() { Value = it.Value, Code = ct });
					}
				}
			}

			return ret;

		}
		//Metoda da brisanje 
		public void DeleteAll()
		{
			using (var db = new ProjectDBContext())
			{
				foreach (ItemTable it in db.One)

					db.Remove(it);
				db.SaveChanges();
			}
			using (var db2 = new ProjectDBContext2())
			{
				foreach (ItemTable it in db2.Two)

					db2.Remove(it);
				db2.SaveChanges();
			}
			using (var db3 = new ProjectDBContext3())
			{
				foreach (ItemTable it in db3.Three)

					db3.Remove(it);
				db3.SaveChanges();
			}
			using (var db4 = new ProjectDBContext4())
			{
				foreach (ItemTable it in db4.Four)

					db4.Remove(it);
				db4.SaveChanges();
			}
		}
	}
}
