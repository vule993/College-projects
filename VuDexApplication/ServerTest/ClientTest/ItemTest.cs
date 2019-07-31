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
    class ItemTest
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase(true, 4, "Name", 2)]
        [TestCase(false, 0, "Name", 0)]
        [TestCase(true, Double.MaxValue, "Name", Double.MaxValue)]
        public void Item_ConstructorTest_GoodParameters(bool active, double destructivePower, String name, double quantity)
        {
            Item item = new Item(active, destructivePower, name, quantity);

            Assert.AreEqual(active, item.Active);
            Assert.AreEqual(destructivePower, item.DestructivePower);
            Assert.AreEqual(name, item.Name);
            Assert.AreEqual(quantity, item.Quantity);
            
        }

        [Test]
        [TestCase(true, -3, "Name", 4)]
        [TestCase(false, 0, "Name", -30)]
        [TestCase(true, Double.MaxValue + Double.MaxValue, "Name", Double.MaxValue)]
        public void Item_ConstructorTest_ArgumentOutOfRangeException_BadParameters(bool active, double destructivePower, String name, double quantity)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                Item item = new Item(active, destructivePower, name, quantity);
            });
        }

        [Test]
        [TestCase(true, 1, null, 1)]
        public void Item_ConstructorTest_ArgumentNullException_BadParameters(bool active, double destructivePower, String name, double quantity)
        {
            Assert.Throws<ArgumentNullException>(() => {
                Item item = new Item(active, destructivePower, name, quantity);
            });
        }

        [Test]
        [TestCase(true, 1, "", 1)]
        public void Item_ConstructorTest_ArgumentException_BadParameters(bool active, double destructivePower, String name, double quantity)
        {
            Assert.Throws<ArgumentException>(() => {
                Item item = new Item(active, destructivePower, name, quantity);
            });
        }
    }
}
