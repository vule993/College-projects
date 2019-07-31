using NUnit.Framework;
using QueueApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ServiceTest
{
    [TestFixture]
    public class DbQueueTest
    {
        [Test]
        [TestCase("DbPrviRed")]
        [TestCase("DbDrugiRed")]
        public void DbQueueConstructor_GoodParameters(String title)
        {
            DbQueue dbQueue = new DbQueue(title);

            Assert.AreEqual(title, dbQueue.Title);
        }

        [Test]
        [TestCase("")]
        public void DbQueueConstructor_BasParameters_ArgumentException(String title)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                DbQueue dbQueue = new DbQueue(title);
            });
        }

        [Test]
        [TestCase(null)]
        public void DbConstructor_BasParameters_ArgumentNullException(String title)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                DbQueue dbQueue = new DbQueue(title);
            });
        }
    }
}
