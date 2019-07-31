using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace QueueApp.Common
{
    
    public interface IUserPresenter
    {
        void SubscribeCommand(String queueName);

        void CreateCommand(String queueName);

        void UpdateCommand();
        void ListeningOnClientQueue(ConcurrentQueue<String> queue);
    }
}
