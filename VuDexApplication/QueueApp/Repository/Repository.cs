using QueueApp.Common;
using QueueApp.Model;
using QueueApp.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace QueueApp.Repository
{
    public class Repository : IRepository
    {
        #region Fields
        private Server destinationServer;
        private UserContext userContext;
        private XmlProvider xmlProvider = new XmlProvider();
        private QueueProvider queueProvider = new QueueProvider();
        #endregion

        #region Properties
        public Server DestinationServer { get { return destinationServer; } set { destinationServer = value; } }
        public UserContext UserContext { get { return userContext; } set { userContext = value; } }
        #endregion

        public Repository(Server s, UserContext userContext)
        {
            if (s == null)
                throw new ArgumentNullException("Server ne moze biti null.");

            if(userContext == null)
                throw new ArgumentNullException("UserContext ne moze biti null.");

            destinationServer = s;
            this.userContext = userContext;

            var users = GetAll().ToList();

            Dictionary<String, ClientQueue> queues = DestinationServer.ClientQueues;

            foreach(var user in users)
            {
                user.IsListening = false;
                if (!queues.ContainsKey(user.SubscribedTo))
                {
                    queues.Add(user.SubscribedTo, new ClientQueue(user.SubscribedTo));
                    Task.Factory.StartNew(() => DestinationServer.ListeningOnClientQueue(DestinationServer.ClientQueues[user.SubscribedTo].QueueA));
                }

                queues[user.SubscribedTo].SubscribedClients.Add(user);
            }


            Task.Factory.StartNew(() => ListeningDbQueue());   
        }

        
        #region Listeners
        private void ListeningDbQueue()
        {
            while (true)
            {
                
                if (DestinationServer.DbQueue.QueueA.Count > 0)
                {
                    String xml;
                    xml = queueProvider.Pop(DestinationServer.DbQueue.QueueA);
                    //send to method deserialize
                    Packet packet = xmlProvider.Deserialize(xml);
                    User user = packet.User;

                    if(packet.TypeOfRequest != RequestType.UPDATE)
                    {
                        User createdUser = Add(user);
                        packet.User = createdUser;
                        packet.Message = "Add";
                    }
                    else
                    {
                        Update(user);
                        packet.Message = "Update";
                    }

                    xml = xmlProvider.Serialize(packet);
                    queueProvider.Push(DestinationServer.DbQueue.QueueB, xml);

                    //Console.WriteLine($"Recieved message: {recievedPacket}");
                }
                Thread.Sleep(5000);
            }
        }
        #endregion

        #region Operations on DB
        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> query = userContext.Set<User>();
            return query;
        }

        public User Add(User element)
        {
            User user = userContext.Set<User>().Add(element);
            Save();
            return user;
        }

        public void Update(User element)
        {

            //userContext.Entry(element).State = EntityState.Modified;
            userContext.SaveChangesAsync();
            //Save();
        }

        public int Save()
        {
            return userContext.SaveChanges();
        }
        #endregion
    }
}
