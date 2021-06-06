using Common;
using LoadBalancer;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancerTest
{
    [TestFixture]
    public class WorkerTest
    {
        Mock<Worker> _worker;
        [SetUp]
        public void SetUp()
        {
            _worker = new Mock<Worker>();
        }

        [Test]
        [TestCase(-1)]
        public void ObradaLosiParametri( int wid)
        {
            ListDescription pom = new ListDescription();
            pom.List = null;
            Assert.Throws<ArgumentException>(
              () =>
              {
                  _worker.Object.Obrada(pom,wid);
              });
        }
        [Test]
        [TestCase(null, 1)]
        public void ObradaLosiParametri(ListDescription ld, int wid)
        {
            Assert.Throws<ArgumentNullException>(
              () =>
              {
                  _worker.Object.Obrada(ld, wid);
              });
        }

        [Test]
        public void WorkerKonstruktorDobarTest()
        {
            Worker.Count = 0;
            Worker w = new Worker();
            Assert.AreEqual(0, w.Id);
            Assert.AreEqual(1, Worker.Count);
        }
        [Test]
        [TestCase(-1,3, "10/10/2020 20:20:20", "10/11/2020 20:20:20")]
        [TestCase(0, -3, "10/10/2020 20:20:20", "10/11/2020 20:20:20")]
        [TestCase(0, 8, "10/10/2020 20:20:20", "10/11/2020 20:20:20")]
        [TestCase(0, 3, "10/10/2019 20:20:20", "10/11/2018 20:20:20")]
        [TestCase(0, 3, "10/10/2099 20:20:20", "10/11/2099 20:20:20")]
        [TestCase(-1, -10, "10/10/2099 20:20:20", "10/11/2199 20:20:20")]
        public void ItemsIntervalLosiParametri(int workerId, Code code, DateTime from, DateTime to)
        {
            Assert.Throws<ArgumentException>(
              () =>
              {
                  _worker.Object.ItemsInterval(workerId,code,from,to);
              });
        }        
    }
}
