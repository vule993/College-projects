using QueueApp.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueApp.Common
{
    public interface IUserModel
    {
        void SubscribeCommand(String queueName);

        void CreateCommand(String queueName);

        void UpdateCommand();

        void ListeningOnClientQueue(ConcurrentQueue<String> queue);

        /*
        List<User> GetAll();
        User GetOne(int id);
        void Insert(User user);
        void Update(User user);
        void Delete(User user);
        void SaveChanges();
        */
    }
}
