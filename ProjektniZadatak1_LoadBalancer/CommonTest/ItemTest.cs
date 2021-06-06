using Common;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTest
{
    [TestFixture]
    public class ItemTest
    {
        [Test]
        [TestCase(2,10)]
        public void KonstruktorDobriParametriTest(Code c, double v)
        {
            Item item = new Item(c, v);
            Assert.AreEqual(c, item.Code);
            Assert.AreEqual(v, item.Value);
        }

        [Test]
        [TestCase(-10, -1)]
        [TestCase(10, -500)]
        [TestCase(8, -500)]
        [TestCase(10, 500)]
        [TestCase(-10, 1)]
        [TestCase(4, -1)]
        public void KonstruktorLosiParametriTest(Code c, double v)
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    Item item = new Item(c, v);
                });
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(7, 0)]
        public void KonstruktorGranicniParametriTest(Code c, double v)
        {
            Item item = new Item(c, v);
            Assert.AreEqual(c, item.Code);
            Assert.AreEqual(v, item.Value);
        }      
    }
}
