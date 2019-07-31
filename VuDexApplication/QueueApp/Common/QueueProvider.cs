using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueApp.Common
{
    public class QueueProvider
    {
        #region Queue operations
        public String Pop(ConcurrentQueue<String> queue)
        {
            if(queue == null)
            {
                throw new ArgumentNullException("Red ne moze biti null.");
            }

            String xml;
            queue.TryDequeue(out xml);
            return xml;
        }

        public void Push(ConcurrentQueue<String> queue, String text)
        {
            if(queue == null)
            {
                throw new ArgumentNullException("Red ne moze biti null.");

            }

            if (text == null)
            {
                throw new ArgumentNullException("Poruka ne moze biti null.");
            }

            if (text == "")
            {
                throw new ArgumentException("Poruka ne moze biti prazna.");
            }

            queue.Enqueue(text);
        }
        #endregion
    }
}
