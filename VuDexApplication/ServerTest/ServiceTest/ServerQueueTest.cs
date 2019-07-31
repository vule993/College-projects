using Moq;
using NUnit.Framework;
using QueueApp.Common;
using QueueApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ServiceTest
{
    [TestFixture]
    public class ServerQueueTest
    {
        [Test]
        [TestCase("PrviRed")]
        [TestCase("DrugiRed")]
        public void ServerQueueConstructor_GoodParameters(String title)
        {
            ServerQueue serverQueue = new ServerQueue(title);

            Assert.AreEqual(title, serverQueue.Title);
        }

        [Test]
        [TestCase("")]
        public void ServerQueueConstructor_BasParameters_ArgumentException(String title)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                ServerQueue serverQueue = new ServerQueue(title);
            });
        }

        [Test]
        [TestCase(null)]
        public void ServerQueueConstructor_BasParameters_ArgumentNullException(String title)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ServerQueue serverQueue = new ServerQueue(title);
            });
        }
    }
}
