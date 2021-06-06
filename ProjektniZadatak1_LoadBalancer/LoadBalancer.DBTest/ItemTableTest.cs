using LoadBalancer.DB;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.DBTest
{
    [TestFixture]
    public class ItemTableTest
    {
        [Test]
        [TestCase("CODE_L",34,"10/10/2020 20:20:20",1)]
        [TestCase("CODE_LIMITSET", -65, "10/10/2020 20:20:20", 1)]
        [TestCase("CODE_LIMITSET", 34, "10/10/2099 20:20:20", 1)]
        [TestCase("CODE_LIMITSET", 34, "10/10/2020 20:20:20",-5)]
        public void ItemTableKonstruktorLosiParametri(string code, double value, DateTime timeStamp, int workerId)
        {
            Assert.Throws<ArgumentException>(
              () =>
              {
                  ItemTable it = new ItemTable(code, value, timeStamp, workerId);
              });
        }

        [Test]
        [TestCase("CODE_ANALOG", 34, "10/10/2020 20:20:20", 5)]
        [TestCase("CODE_DIGITAL", 34, "10/10/2020 20:20:20", 5)]
        [TestCase("CODE_LIMITSET", 34, "10/10/2020 20:20:20", 5)]
        [TestCase("CODE_SINGLENODE", 34, "10/10/2020 20:20:20", 5)]
        [TestCase("CODE_MULTIPLENODE", 34, "10/10/2020 20:20:20", 5)]
        [TestCase("CODE_CUSTOM", 34, "10/10/2020 20:20:20", 5)]
        [TestCase("CODE_CONSUMER", 34, "10/10/2020 20:20:20", 5)]
        [TestCase("CODE_SOURCE", 34, "10/10/2020 20:20:20", 5)]
        public void ItemTableKonstruktorDobriParametri(string code, double value, DateTime timeStamp, int workerId)
        {
            ItemTable it = new ItemTable(code,value,timeStamp,workerId);
            Assert.AreEqual(code, it.Code);
            Assert.AreEqual(value, it.Value);
            Assert.AreEqual(timeStamp, it.TimeStamp);
            Assert.AreEqual(workerId, it.WorkerId);
        }

        [Test]
        [TestCase("CODE_LIMITSET", 0, "10/10/2020 20:20:20", 1)]
        [TestCase("CODE_LIMITSET", 34, "10/10/2020 20:20:20", 0)]
        [TestCase("CODE_ANALOG", 34, "10/10/2020 20:20:20", 34)]
        public void ItemTableKonstruktorGranicniParametri(string code, double value, DateTime timeStamp, int workerId)
        {
            ItemTable it = new ItemTable(code, value, timeStamp, workerId);
            Assert.AreEqual(code, it.Code);
            Assert.AreEqual(value, it.Value);
            Assert.AreEqual(timeStamp, it.TimeStamp);
            Assert.AreEqual(workerId, it.WorkerId);
        }
    }
}
