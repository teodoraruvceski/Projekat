using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
		private string txtFileName;
		StreamWriter streamWriter;

		public string TxtFileName { get => txtFileName; set => txtFileName = value; }
		[ExcludeFromCodeCoverage]
        public StreamWriter StreamWriter { get => streamWriter; set => streamWriter = value; }

        public Log(string txtFileName)
		{
			if(txtFileName==null )
			{
				throw new ArgumentNullException("Argumenti ne mogu biti null.");				
			}
			else if(!txtFileName.Contains(@"\"))
			{
				throw new ArgumentException("Pogresan format stringa.");
			}
			else
			{
				this.TxtFileName = txtFileName;
			}				
		}
		/// <summary>
		/// Metoda za logovanje rada workera
		/// </summary>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <param name="dt"></param>
		/// <param name="worker"></param>
		[ExcludeFromCodeCoverage]
		public void Write(Code code, double value, DateTime dt, int worker)
		{
			if(code<0 || value<0 || worker<0)
			{
				throw new ArgumentException("Argumenti ne smeju biti negativni");
			}
			else if(code>(Code)8)
			{
				throw new ArgumentException("Kod ne sme biti veci od 7");
			}
			else if(dt>DateTime.Now)
            {
				throw new ArgumentException("Datum je pogresan");
			}
			else
			{
				string t = "\n" + dt.ToString() + ": WORKER " + worker + " SAVED -> Code: " + code.ToString() + ", Value: " + value;
				UpisUFajl(t);
			}
		}
		/// <summary>
		/// Metoda za logovanje aktivnosti writera
		/// </summary>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <param name="dt"></param>
		[ExcludeFromCodeCoverage]
		public void Write(Code code, double value, DateTime dt)
		{
			if (code < 0 || value < 0)
			{
				throw new ArgumentException("Argumenti ne smeju biti negativni");
			}
			else if (code > (Code)8)
			{
				throw new ArgumentException("Kod ne sme biti veci od 7");
			}
			else if (dt > DateTime.Now)
			{
				throw new ArgumentException("Datum je pogresan");
			}
			else
			{
				using (StreamWriter = new StreamWriter(TxtFileName, true))
				{
					StreamWriter.WriteLine();
					StreamWriter.Close();
				}
				string t = "\n" + dt.ToString() + ": WRITER SENT -> Code: " + code.ToString() + ", Value: " + value;
				UpisUFajl( t);
			}
			
		}
		/// <summary>
		/// Logovanje za kraj rada aplikacije
		/// </summary>
		/// <param name="dt"></param>
		[ExcludeFromCodeCoverage]
		public void WriteTurnOff(DateTime dt)
		{
			if (dt > DateTime.Now)
			{
				throw new ArgumentException("Datum je pogresan");
			}
			string t = "\n" + dt.ToString() + ": Application CLOSED\n" + "===============================================================================================";
			UpisUFajl( t);
		}
		/// <summary>
		/// Logovanje kada se desi neko citanje iz baze
		/// </summary>
		/// <param name="dtFrom"></param>
		/// <param name="dtTo"></param>
		/// <param name="dtWhen"></param>
		/// <param name="worker"></param>
		[ExcludeFromCodeCoverage]
		public void Read(DateTime dtFrom, DateTime dtTo, DateTime dtWhen, int worker,Code code)
		{
			if(worker<0)
			{
				throw new ArgumentException("Argumenti ne smeju biti negativni");
			}
			else if (dtFrom > DateTime.Now || dtWhen > DateTime.Now || dtFrom>dtTo)
			{
				throw new ArgumentException("Datum je pogresan");
			}
			
			string t = "\n" + dtWhen.ToString() + ": WRITER SEARCHED DATA-> Worker_" + worker + " data from: " + dtFrom.ToString() + " to: " + dtTo.ToString()+ ", code: "+ code.ToString();
			UpisUFajl(t);		
		}
		/// <summary>
		/// Metoda za upis u fajl
		/// </summary>
		/// <param name="tekst"></param>
		[ExcludeFromCodeCoverage]
		public void UpisUFajl(string tekst)
        {
			using (StreamWriter = new StreamWriter(txtFileName, true))
            {
				StreamWriter.WriteLine(tekst);
			}				
        }
	}
}
