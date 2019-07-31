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
    class PositionTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase(1, 1000, 3)]
        [TestCase(-1, -500, 3)]
        public void Position_ConstructorTest_GoodParameters(double x, double y, double z)
        {
            Position position = new Position(x, y, z);

            Assert.AreEqual(x, position.X);
            Assert.AreEqual(y, position.Y);
            Assert.AreEqual(z, position.Z);

        }

        [Test]
        [TestCase(Double.MaxValue + Double.MaxValue, 1000, 3)]
        [TestCase(Double.MinValue + Double.MinValue, -500, 3)]
        public void Position_ConstructorTest_ArgumentOutOfRangeException_BadParameters(double x, double y, double z)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                Position position = new Position(x, y, z);
            });
        }

    }
}
