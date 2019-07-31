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
    public class ServerTest
    {
        Mock<Packet> packetMock;
        Server server;

        [SetUp]
        public void Setup()
        {
            packetMock = new Mock<Packet>();
            server = new Server();
        }

        #region UpdateTest
        [Test]
        [TestCase("Nunc eleifend magna quis sapien finibus, ac laoreet urna euismod.")]
        [TestCase("Pellentesque sagittis est sit amet nibh dictum, quis finibus magna tristique.")]
        public void Update_InputIsValid(String text)
        {
            server.Update(text);

            Assert.AreEqual(1, server.DbQueue.QueueA.Count);
        }

        [Test]
        [TestCase("")]
        public void Update_InputIsNotValid_ArgumentException(String text)
        {
            Assert.Throws<ArgumentException>(() => 
            {
                server.Update(text);
            });
        }

        [Test]
        [TestCase(null)]
        public void Update_InputIsNotValid_ArgumentNUllException(String text)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                server.Update(text);
            });
        }
        #endregion

        #region SubscribeTest
        [Test]
        [TestCase("Suspendisse ")]
        [TestCase("Consectetur ")]
        [TestCase("Aenean")]
        public void Subscribe_InputIsValid_ClientQueueExist(String nazivReda)
        {
            packetMock.Object.Message = nazivReda;

            server.ClientQueues.Add(nazivReda, new ClientQueue(nazivReda));
            Assert.AreEqual(true, server.Subscribe(packetMock.Object));
        }

        [Test]
        [TestCase("Molestie")]
        [TestCase("Efficitur")]
        [TestCase("Aenean")]
        public void Subscribe_InputIsValid_ClientQueueNotExist(String nazivReda)
        {
            packetMock.Object.Message = nazivReda;

            Assert.AreEqual(false, server.Subscribe(packetMock.Object));
        }

        [Test]
        public void Subscribe_InputIsNotValid_ArgumentNullException()
        {
            // 'Object reference not set to an instance of an object.'
            // packetMock was null.
            /*
            packetMock = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                server.Subscribe(packetMock.Object);
            });
            */
            Packet packet = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                server.Subscribe(packet);
            });
        }
        #endregion

        #region CreateQueueTest
        [Test]
        [TestCase("Curabitur")]
        [TestCase("Fermentum")]
        public void CreateQueue_InputIsValid_ClientQueueNotExist(String nazivReda)
        {
            packetMock.Object.Message = nazivReda;

            Assert.AreEqual(true, server.CreateQueue(packetMock.Object));
        }

        [Test]
        [TestCase("Phasellus")]
        [TestCase("Fringilla ")]
        public void CreateQueue_InputIsValid_ClientQueueExist(String nazivReda)
        {
            packetMock.Object.Message = nazivReda;

            server.ClientQueues.Add(nazivReda, new ClientQueue(nazivReda));

            Assert.AreEqual(false, server.CreateQueue(packetMock.Object));
        }

        [Test]
        public void CreateQueue_InputIsNotValid()
        {
            Packet packet = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                server.CreateQueue(packet);
            });
        }
        #endregion
    }
}
