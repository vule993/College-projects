using Moq;
using NUnit.Framework;
using QueueApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ClientTest
{
    [TestFixture]
    class PacketTest
    {
        Mock<User> userMock;

        [SetUp]
        public void Setup()
        {
            userMock = new Mock<User>();
        }

        [Test]
        [TestCase(RequestType.CREATE, "Poruka")]
        public void Packet_ConstructorTest_GoodParameters(RequestType command, String message)
        {
            Packet p = new Packet(message, command, userMock.Object);

            Assert.AreEqual(userMock.Object, p.User);
            Assert.AreEqual(command, p.TypeOfRequest);
            Assert.AreEqual(message, p.Message);
        }

        [Test]
        [TestCase(RequestType.CREATE, null)]
       
        public void PacketTest_NullMessageException_BadParameters(RequestType command, String message)
        {
            Assert.Throws<ArgumentNullException>(() => {
                Packet p = new Packet(message, command, userMock.Object);
            });

        }
        [Test]
        [TestCase(RequestType.CREATE, "poruka", null)]
        public void PacketTest_NullUserException_BadParameters(RequestType command, String message, User user)
        {
            Assert.Throws<ArgumentNullException>(() => {
                Packet p = new Packet(message, command, user);
            });

        }

        [Test]
        [TestCase(RequestType.CREATE, "")]
        public void PacketTest_EmptyString_BadParameters(RequestType command, String message)
        {
            Assert.Throws<ArgumentException>(() => {
                Packet p = new Packet(message, command, userMock.Object);
            });

        }
    }
}
