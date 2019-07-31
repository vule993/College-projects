using Moq;
using NUnit.Framework;
using QueueApp.Common;
using QueueApp.Repository;
using QueueApp.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.RepositoryTest
{
    [TestFixture]
    public class RepositoryTest
    {
        Mock<UserContext> userContextMock;
        Mock<Server> serverMock;
        Mock<User> userMock;

        [SetUp]
        public void Setup()
        {
            userContextMock = new Mock<UserContext>();
            serverMock = new Mock<Server>();
            userMock = new Mock<User>();
        }
        /*
        [Test]
        public void RepositoryConstructor_GoodParameters()
        {
            userContextMock.Object.Users.Add(new User());


            Repository repository = new Repository(serverMock.Object, userContextMock.Object);

            Assert.AreEqual(userContextMock.Object, repository.UserContext);
        }
        */

        [Test]
        public void RepositoryConstructor_ArgumentNullException_Server_BadParameters()
        {
            Server s = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                Repository repo = new Repository(s,userContextMock.Object);
            });
        }

        [Test]
        public void RepositoryConstructor_ArgumentNullException_UserContext_BadParameters()
        {
            UserContext uc = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                Repository repo = new Repository(serverMock.Object, uc);
            });
        }


        [Test]
        public void GetAll()
        {
            var expectedUsers = new List<User>
            {
                new User()
            };

            var mockUserSet = GetMockDbSet(expectedUsers.AsQueryable());

            var mockUserContext = new Mock<UserContext>();
            mockUserContext.Setup(o => o.Users).Returns(mockUserSet.Object);

            // test the controller GetPersons() method, which leverages Include()

            //var controller = new PersonsController(mockUserContext.Object);
            var actualUsers = mockUserContext.Object.Users.ToList();
            CollectionAssert.AreEqual(expectedUsers, actualUsers.ToList());
        }

        private Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
            return mockSet;
        }
    }
}
