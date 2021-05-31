using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	/// <summary>
	/// Klasa koja je zaduzena za upis aktivnosti programa u fajl.
	/// </summary>
	public class Log
	{
		string txtFileName;
		StreamWriter streamWriter;

		public Log(string txtFileName)
		{
			this.txtFileName = txtFileName;
		}



		public void Write(Code code, double value, DateTime dt, int worker = -1)
		{
			using (streamWriter = new StreamWriter(txtFileName, true))
			{

				if (worker == -1)
				{
					streamWriter.WriteLine("\n" + dt.ToString() + ": WRITER SENT -> Code: " + code.ToString() + ", Value: " + value);
					streamWriter.Close();
				}
				else
				{
					streamWriter.WriteLine("\n" + dt.ToString() + ": WORKER " + worker + " SAVED -> Code: " + code.ToString() + ", Value: " + value);
					streamWriter.Close();

				}
			}
		}
		public void Write(DateTime dt)
		{
			using (streamWriter = new StreamWriter(txtFileName, true))
			{
				streamWriter.WriteLine("\n" + dt.ToString() + ": Application CLOSED\n");
				streamWriter.WriteLine("\n==================================================================");
				streamWriter.Close();
			}
		}

		public void Read()
		{

		}
	}
}
