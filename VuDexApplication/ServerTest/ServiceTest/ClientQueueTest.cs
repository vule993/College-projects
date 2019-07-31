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
    public class ClientQueueTest
    {
        [Test]
        [TestCase("PrviRed")]
        [TestCase("DrugiRed")]
        public void ClientQueueConstructor_GoodParameters(String title)
        {
            ClientQueue clientQueue = new ClientQueue(title);

            Assert.AreEqual(title, clientQueue.Title);
        }

        [Test]
        [TestCase("")]
        //[ExpectedException(typeof(ArgumentException))]
        public void ClientQueueConstructor_BadParameters_ArgumentException(String title)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                ClientQueue clientQueue = new ClientQueue(title);
            });
        }

        [Test]
        [TestCase(null)]
        public void ClientQueueConstructor_BadParameters_ArgumentNullException(String title)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ClientQueue clientQueue = new ClientQueue(title);
            });
        }
    }
}
