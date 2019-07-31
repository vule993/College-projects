using NUnit.Framework;
using QueueApp.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ServiceTest
{
    [TestFixture]
    public class QueueProviderTest
    {
        #region PushTest
        [Test]
        [TestCase("Ovo je neka poruka")]
        [TestCase("Poruka")]
        [TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer egestas.")]
        public void Push_InputIsValid(String message)
        {
            var queue = new ConcurrentQueue<String>();
            var queueProvider = new QueueProvider();

            queueProvider.Push(queue, message);

            String value;
            queue.TryDequeue(out value);

            Assert.AreEqual(message, value);
        }

        [Test]
        [TestCase("")]
        public void Push_InputIsNotValid_ArgumentException(String message)
        {
            var queue = new ConcurrentQueue<String>();
            var queueProvider = new QueueProvider();

            Assert.Throws<ArgumentException>(() => 
            {
                queueProvider.Push(queue, message);
            });
        }

        [Test]
        [TestCase(null)]
        public void Push_InputIsNotValid_ArgumentNullException(String message)
        {
            var queue = new ConcurrentQueue<String>();
            var queueProvider = new QueueProvider();

            Assert.Throws<ArgumentNullException>(() =>
            {
                queueProvider.Push(queue, message);
            });
        }

        [Test]
        [TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing.")]
        [TestCase("Lorem ipsum dolor sit.")]
        [TestCase(null)]
        public void Push_QueueIsNotValid_ArgumentNullException(String message)
        {
            ConcurrentQueue<String> queue = null;
            var queueProvider = new QueueProvider();

            Assert.Throws<ArgumentNullException>(() =>
            {
                queueProvider.Push(queue, message);
            });
        }
        #endregion

        #region PopTest
        [Test]
        [TestCase("Lorem Ipsum is simply dummy text of the printing and typesetting industry.")]
        [TestCase("Phasellus efficitur sapien ut neque porttitor fringilla.")]
        public void Pop_QueueIsValid(String message)
        {
            var queue = new ConcurrentQueue<String>();
            var queueProvider = new QueueProvider();

            queue.Enqueue(message);

            String value = queueProvider.Pop(queue);

            Assert.AreEqual(message, value);
        }

        [Test]
        [TestCase("Nullam sollicitudin, ipsum sed lobortis maximus")]
        [TestCase("Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae")]
        public void Pop_QueueIsNotValid(String message)
        {
            ConcurrentQueue<String> queue = null;
            var queueProvider = new QueueProvider();

            Assert.Throws<ArgumentNullException>(() => 
            {
                String value = queueProvider.Pop(queue);
            });
        }
        #endregion
    }
}
