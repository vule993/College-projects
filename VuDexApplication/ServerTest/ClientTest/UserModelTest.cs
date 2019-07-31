using Moq;
using NUnit.Framework;
using QueueApp.Common;
using QueueApp.Model;
using QueueApp.Presenter;
using QueueApp.Service;
using QueueApp.View;
using System;

namespace Test.ClientTest
{
    [TestFixture]
    class UserModelTest
    {
        Mock<User> userMock;
        Mock<UserModel> userModelMock;
        Mock<UserPresenter> userPresenterMock;
        Mock<Server> serverMock;
        Mock<UserView> userViewMock;

        [SetUp]
        public void Setup()
        {
            userMock = new Mock<User>();

            userViewMock = new Mock<UserView>();
            serverMock = new Mock<Server>();
            userModelMock = new Mock<UserModel>(serverMock.Object, userMock.Object);
            userPresenterMock = new Mock<UserPresenter>(userViewMock.Object, userModelMock.Object);
        }

        #region Constructor
        [Test]
        public void UserModel_ConstructorTest_GoodParameters()
        {
            UserModel um = new UserModel(serverMock.Object, userMock.Object);

            Assert.AreEqual(serverMock.Object, um.DestinationServer);
            Assert.AreEqual(userMock.Object, um.User);
        }
        #endregion

        #region SubscribeTest

        //da li je pozvana?
        [Test]
        [TestCase("Poruka")]
        public void SubscribeCommandTest_GoodParameters(String message)
        {
            //userModelMock.Setup(x => x.SubscribeCommand(message));
            
            userPresenterMock.Object.SubscribeCommand(message);
            userModelMock.Verify(x => x.SubscribeCommand(message), Times.Once);
        }

        //null parametar
        [Test]
        [TestCase(null)]
        public void SubscribeCommandTest_NullException_BadParameters(String message)
        {
            UserModel um = new UserModel(serverMock.Object, userMock.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                um.SubscribeCommand(message);
            });
        }

        //empty string parametar
        [Test]
        [TestCase("")]
        public void SubscribeCommandTest_EmptyString_BadParameters(String message)
        {
            UserModel um = new UserModel(serverMock.Object, userMock.Object);
            Assert.Throws<ArgumentException>(() =>
            {
                um.SubscribeCommand(message);
            });
        }
        #endregion

        #region CreateTest
        [Test]
        [TestCase("Poruka")]
        public void UserModel_CreateCommandTest_GoodParameters(String message)
        {
            //userModelMock.Setup(x => x.SubscribeCommand(message));
            UserPresenter up = new UserPresenter(userViewMock.Object, userModelMock.Object);
            //userPresenterMock.Object.CreateCommand(message);
            up.CreateCommand(message);
            userModelMock.Verify(x => x.CreateCommand(message), Times.Once);
        }

        //null parametar
        [Test]
        [TestCase(null)]
        public void UserModel_CreateCommandTes_NullException_BadParameters(String message)
        {
            UserModel um = new UserModel(serverMock.Object, userMock.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                um.CreateCommand(message);
            });
        }

        //empty string parametar
        [Test]
        [TestCase("")]
        public void UserModel_CreateCommandTest_EmptyString_BadParameters(String message)
        {
            UserModel um = new UserModel(serverMock.Object, userMock.Object);
            Assert.Throws<ArgumentException>(() =>
            {
                um.CreateCommand(message);
            });
        }
        #endregion

        #region UpdateTest
        [Test]
        public void UserModel_UpdateCommandTest_GoodParameters()
        {
            //userModelMock.Setup(x => x.SubscribeCommand(message));

            userPresenterMock.Object.UpdateCommand();
            userModelMock.Verify(x => x.UpdateCommand(), Times.Once);
        }
        #endregion

        #region PacketCreate
        [Test]
        [TestCase(RequestType.CREATE, "Poruka")]
        public void PacketCreate_Test_GoodParameters(RequestType command, String message)
        {
            UserModel um = new UserModel(serverMock.Object, userMock.Object);

            Packet paket = um.PacketCreate(command, message, userMock.Object);
            Assert.AreEqual(command, paket.TypeOfRequest);
            Assert.AreEqual(message, paket.Message);
            Assert.AreEqual(userMock.Object, paket.User);
        }

        [Test]
        [TestCase(RequestType.CREATE, null)]
        public void PacketCreate_Test_Null_Message_Exception_BadParameters(RequestType command, String message)
        {
            UserModel um = new UserModel(serverMock.Object, userMock.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                um.PacketCreate(command, message, userMock.Object);
            });
        }

        [Test]
        [TestCase(RequestType.CREATE, "Message", null)]
        [TestCase(RequestType.CREATE, "MessageMessageMessageMessageMessage", null)]
        public void PacketCreate_Test_Null_User_Exception_BadParameters(RequestType command, String message, User user)
        {
            UserModel um = new UserModel(serverMock.Object, userMock.Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                um.PacketCreate(command, message, user);
            });
        }

        //empty string parametar
        [Test]
        [TestCase(RequestType.CREATE, "")]
        public void PacketCreate_Test_EmptyString_BadParameters(RequestType command, String message)
        {
            UserModel um = new UserModel(serverMock.Object, userMock.Object);
            
            Assert.Throws<ArgumentException>(() =>
            {
                um.PacketCreate(command, message, userMock.Object);
            });
        }
        #endregion


    }
}
