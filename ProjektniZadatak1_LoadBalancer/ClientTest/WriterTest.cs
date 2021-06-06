using Client;
using Common;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    [TestFixture]
    public class WriterTest
    {
        Mock<Writer> _writer;
        [SetUp]
        public void SetUp()
        {
            _writer = new Mock<Writer>();
        }

        [Test]
        public void WriterKonstruktorDobarTest()
        {
            Writer w = new Writer();
            Assert.IsNotNull(w.Log);
        }      
    }
}
