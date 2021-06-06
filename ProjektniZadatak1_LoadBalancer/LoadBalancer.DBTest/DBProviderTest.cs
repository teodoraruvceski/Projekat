using Common;
using LoadBalancer.DB;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.DBTest
{
    
    [TestFixture]
    class DBProviderTest
    {
        Mock<DBProvider> _db;
        Mock<ProjectDBContext> _context1;

        [SetUp]
        public void SetUp()
        {
            _db = new Mock<DBProvider>();
            _context1 = new Mock<ProjectDBContext>();
        }

        [Test]
        public void DBProviderKonstruktorDobarTest()
        {
            DBProvider db = new DBProvider();
            Assert.AreNotEqual(db.Log,null );
            Assert.AreEqual(db.Log.TxtFileName, @"FILElog\writerLogServer.txt");
        }             
    }
}
