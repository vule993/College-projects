using Moq;
using NUnit.Framework;
using QueueApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ServiceTest
{
    [TestFixture]
    public class XmlProviderTest
    {
        Mock<User> userMock;
        XmlProvider xmlProvider;

        [SetUp]
        public void Setup()
        {
            xmlProvider = new XmlProvider();
            userMock = new Mock<User>();
        }

        #region SerializeTest
        [Test]
        [TestCase("Etiam in leo et risus cursus mollis.", RequestType.CREATE)]
        [TestCase("Quisque pharetra urna sit amet orci vehicula, ut blandit diam mattis.", RequestType.SUBSCRIBE)]
        [TestCase("Ut euismod diam ut risus elementum facilisis.", RequestType.UPDATE)]
        public void Serialize_PacketIsValid(String text, RequestType requestType)
        {
            Packet packet = new Packet()
            {
                Message = text,
                TypeOfRequest = requestType,
                //User = userMock.Object
                User = new User()
            };

            String xml = xmlProvider.Serialize(packet);

            Packet packetDeserijalizovan = xmlProvider.Deserialize(xml);

            Assert.AreEqual(packet.Message, packetDeserijalizovan.Message);
            Assert.AreEqual(packet.TypeOfRequest, packetDeserijalizovan.TypeOfRequest);
            Assert.AreEqual(packet.User, packetDeserijalizovan.User);
        }

        [Test]
        public void Serialize_InputIsNotValid_ArgumentNullException()
        {
            Packet packet = null;

            Assert.Throws<ArgumentNullException>(() => 
            {
                xmlProvider.Serialize(packet);
            });
        }
        #endregion

        #region DeserializeTest
        [Test]
        [TestCase("<?xml version=\"1.0\" encoding=\"utf-16\"?><Packet xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" message=\"Sed semper dui in accumsan suscipit.\" typeofrequest=\"CREATE\"><user id=\"1\"><Items><Item><ItemId>0</ItemId><Active>true</Active><DestructivePower>320</DestructivePower><Name>Item1</Name><Quantity>1</Quantity></Item></Items><Positions><Position><PositionId>0</PositionId><X>7</X><Y>8</Y><Z>9</Z></Position></Positions><IsListening>false</IsListening></user></Packet>")]
        [TestCase("<?xml version=\"1.0\" encoding=\"utf-16\"?><Packet xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" message=\"Aenean sed tellus id arcu volutpat congue id sit amet dolor.\" typeofrequest=\"UPDATE\"><user id=\"2\"><Items><Item><ItemId>0</ItemId><Active>true</Active><DestructivePower>320</DestructivePower><Name>Item1</Name><Quantity>1</Quantity></Item></Items><Positions><Position><PositionId>0</PositionId><X>7</X><Y>8</Y><Z>9</Z></Position></Positions><IsListening>false</IsListening></user></Packet>")]
        [TestCase("<?xml version=\"1.0\" encoding=\"utf-16\"?><Packet xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" message=\"Nunc sit amet tellus at orci hendrerit molestie.\" typeofrequest=\"SUBSCRIBE\"><user id=\"3\"><Items><Item><ItemId>0</ItemId><Active>true</Active><DestructivePower>320</DestructivePower><Name>Item1</Name><Quantity>1</Quantity></Item></Items><Positions><Position><PositionId>0</PositionId><X>7</X><Y>8</Y><Z>9</Z></Position></Positions><IsListening>false</IsListening></user></Packet>")]
        public void Deserialize_InputIsValid(String text)
        {
            Packet packet = xmlProvider.Deserialize(text);

            String xml = xmlProvider.Serialize(packet);

            Assert.AreEqual(text, xml);
        }

        [Test]
        [TestCase("")]
        public void Deserialize_InputIsNotValid_ArgumentException(String text)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                xmlProvider.Deserialize(text);
            });
        }

        [Test]
        [TestCase(null)]
        public void Deserialize_InputIsNotValid_ArgumentNUllException(String text)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                xmlProvider.Deserialize(text);
            });
        }
        #endregion
    }
}
