using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace CommonTest
{
    [TestFixture]
    class LogTest
    {
        Mock<Log> _log;

        [SetUp]
        public void SetUp()
        {
            _log = new Mock<Log>(@"E:\Tea\fax\3.godina\2.sem\res\Projekat-GIT\Projekat\ProjektniZadatak1_LoadBalancer\proba.txt");
        }

        [Test]
        [TestCase(@"\bla")]
        public void KonstruktorDobriParametriTest(string fajl)
        {
            Log log = new Log(fajl);
            Assert.AreEqual(fajl, log.TxtFileName);
        }

        [Test]
        [TestCase(@"bla")]
        [TestCase(@"b/la")]
        public void KonstruktorLosiParametriTest(string fajl)
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    Log log = new Log(fajl);
                });
        }

        [Test]
        [TestCase(null)]
        public void KonstruktorNullParametriTest(string fajl)
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    Log log = new Log(fajl);
                });
        }
       
        [Test]
        [TestCase(-2,-12, "10/10/2020 20:20:20", -2)]
        [TestCase(3, -12, "10/10/2020 20:20:20", -2)]
        [TestCase(10, 100, "10/10/2020 20:20:20", 4)]
        [TestCase(0, -1, "10/10/2020 20:20:20", -1)]
        [TestCase(0, -1, "10/10/2020 20:20:20", 1)]
        [TestCase(1,-1, "10/10/2020 20:20:20",0)]
        [TestCase(1, 1, "10/10/2022 20:20:20", 0)]
        [ExpectedException(typeof(ArgumentException))]
        public void WriteLosiParametriTest(Code code, double value, DateTime dt, int worker)
        {
            Assert.Throws<ArgumentException>(
            () =>{
                _log.Object.Write(code, value, dt, worker);
            });
        }

        [Test]
        [TestCase(-2, -12, "10/10/2020 20:20:20")]
        [TestCase(6, -12, "10/10/2020 20:20:20")]
        [TestCase(10,10, "10/10/2020 20:20:20")]
        [TestCase(1, 1, "10/10/2022 20:20:20")]
        [ExpectedException(typeof(ArgumentException))]
        public void Write2LosiParametriTest(Code code, double value, DateTime dt)
        {
            Assert.Throws<ArgumentException>(
               () =>
               {                  
                   _log.Object.Write(code, value, dt);
               });
        }

        [Test]
        [TestCase("10/10/2020 20:20:20", "10/10/2020 20:20:20", "10/10/2020 20:20:20",-1,0)]
        [TestCase("10/10/2022 20:20:20", "10/10/2020 20:20:20", "10/10/2020 20:20:20", 1,0)]
        [TestCase("10/10/2020 20:20:20", "10/10/2020 20:20:20", "10/10/2022 20:20:20", 1,0)]
        [TestCase("10/10/2016 20:20:20", "10/10/2015 20:20:20", "10/10/2020 20:20:20", 1,0)]
        public void ReadLosiParametri(DateTime dtFrom, DateTime dtTo, DateTime dtWhen, int worker,Code code)
        {
            Assert.Throws<ArgumentException>(
               () =>
               {
                   _log.Object.Read(dtFrom,dtTo,dtWhen, worker,(Code)code);
               });
        }

        [Test]
        [TestCase( "10/10/2099 20:20:20")]
        [ExpectedException(typeof(ArgumentException))]
        public void Write3LosiParametriTest(DateTime dt)
        {
            Assert.Throws<ArgumentException>(
               () =>
               {                  
                   _log.Object.WriteTurnOff(dt);
               });
        }       
    }
}
