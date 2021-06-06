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
    class BalancerServiceTest
    {
       Mock<BalancerService> _bs;
       
       [SetUp]
       public void SetUp()
        {
            _bs =new  Mock<BalancerService>();
        }
       
        [Test]
        [TestCase(2)]
        [TestCase(3)]       
        public void DescriptionKonstruktorDobriParametri(int data)
        {
            Description.count = 0;
            Description d = new Description(data);
            Assert.AreEqual(data, d.DataSet);
            Assert.AreEqual(1, d.id);
            Assert.AreEqual(1, Description.count);

        }

        [Test]
        [TestCase(4)]
        [TestCase(1)]
        public void DescriptionKonstruktorGranicniParametri(int data)
        {
            Description.count = 0;
            Description d = new Description(data);
            Assert.AreEqual(data, d.DataSet);
            Assert.AreEqual(1, d.id);
            Assert.AreEqual(1, Description.count);

        }

        [Test]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(-10)]
        [TestCase(0)]
        public void DescriptionKonstruktorLosiParametri(int data)
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    Description d = new Description(data);
                });

        }

        [Test]
        [TestCase(null)]
        public void ListDescriptionKonstruktorLosiParametri(List<Description> ld)
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    ListDescription d = new ListDescription(ld);
                });

        }

        [Test]
        public void BalancerServiceKonstruktorDobarTest()
        {
            BalancerService bs = new BalancerService();
            Assert.AreNotEqual(null, bs.One);
            Assert.AreNotEqual(null, bs.Two);
            Assert.AreNotEqual(null, bs.Three);
            Assert.AreNotEqual(null, bs.Four);
            Assert.AreEqual(1, bs.One.DataSet);
            Assert.AreEqual(2, bs.Two.DataSet);
            Assert.AreEqual(3, bs.Three.DataSet);
            Assert.AreEqual(4, bs.Four.DataSet);
            Assert.AreNotEqual(null, bs.Workers);
            Assert.AreEqual(1, bs.Workers.Count);
            Assert.AreNotEqual(null, BalancerService.ListDescription);
            Assert.AreEqual(4, BalancerService.ListDescription.List.Count);
        }

        [Test]
        [TestCase(Code.CODE_ANALOG,500)]
        [TestCase(Code.CODE_CONSUMER, 500)]
        [TestCase(Code.CODE_CUSTOM, 500)]
        [TestCase(Code.CODE_DIGITAL, 500)]
        [TestCase(Code.CODE_LIMITSET, 500)]
        [TestCase(Code.CODE_MULTIPLENODE, 500)]
        [TestCase(Code.CODE_SINGLENODE, 500)]
        [TestCase(Code.CODE_SOURCE, 500)]
        public void WriteDobriParametri(Code c, double v)
        {
            Item item = new Item(c, v);
            _bs.Object.Write(item);
            bool prvi =false;
            foreach (Description d in BalancerService.ListDescription.List)
            {
                if (d.ListItem.Count > 0)
                    prvi = true;
            }
            Assert.AreEqual(true, prvi);
        }
        
        [Test]
        public void OnDobarTest()
        {
            //int pom = _bs.Object.Workers.Count();
            _bs.Object.On();
            Assert.AreEqual(2, _bs.Object.Workers.Count());
        }

        [Test]
        public void OnLosTest()
        {
            _bs.Object.Workers = null;
            Assert.Throws<NullReferenceException>(
                () =>
                {
                    _bs.Object.On();
                });
        }

        [Test]
        public void OffDobarTest()
        {
            //int pom = _bs.Object.Workers.Count();
            _bs.Object.Off();
            Assert.AreEqual(0, _bs.Object.Workers.Count());
        }
        
        [Test]
        public void OffLosTest1()
        {
            //int pom = _bs.Object.Workers.Count();
            _bs.Object.Workers = new List<Worker>();
            Assert.Throws<Exception>(
                () =>
                {
                    _bs.Object.Off();
                });
           
        }

        [Test]
        public void OffLosTest2()
        {
            //int pom = _bs.Object.Workers.Count();
            _bs.Object.Workers = null;
            Assert.Throws<NullReferenceException>(
                () =>
                {
                    _bs.Object.Off();
                });

        }

        [Test]
        [TestCase(-1, 3, "10/10/2020 20:20:20", "10/11/2020 20:20:20")]
        [TestCase(0, -3, "10/10/2020 20:20:20", "10/11/2020 20:20:20")]
        [TestCase(0, 8, "10/10/2020 20:20:20", "10/11/2020 20:20:20")]
        [TestCase(0, 3, "10/10/2019 20:20:20", "10/11/2018 20:20:20")]
        public void ItemsIntervalLosiParametri(int workerId, Code code, DateTime from, DateTime to)
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    _bs.Object.ItemsInterval(workerId, code, from, to);
                });
        }

        [Test]
        [TestCase(ExpectedResult = 1)]
        public int NumberOfWorkersDobarTest()
        {
            return _bs.Object.NumberOfWorkers();
        }

        [Test]
        public void NumberOfWorkersLosTest()
        {
            _bs.Object.Workers = null;
            Assert.Throws<NullReferenceException>(
                () =>
                {
                    _bs.Object.NumberOfWorkers();
                });
        }      
    }
}
