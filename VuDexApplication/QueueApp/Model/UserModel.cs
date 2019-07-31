using QueueApp.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using QueueApp.Service;
using System.Threading;

namespace QueueApp.Model
{
    public class UserModel : IUserModel
    {
        public Server DestinationServer { get; set; }
        public User User { get; set; }
        private QueueProvider queueProvider = new QueueProvider();
        private XmlProvider xmlProvider = new XmlProvider();

        public UserModel(Server server, User user)
        {
            if(server == null || user == null)
            {
                throw new ArgumentNullException("Parametri ne smeju biti null.");
            }

            DestinationServer = server;
            User = user;
            
            Task.Factory.StartNew(() => ListeningOnServerQueue());
        }

        #region Commands
        
        public virtual void SubscribeCommand(String queueName)
        {
            if(queueName == null)
            {
                throw new ArgumentNullException("Parametar ne sme biti null.");
            }
            if (queueName == "")
            {
                throw new ArgumentException("Parametar ne sme biti prazan string.");
            }
            Packet packet = PacketCreate(RequestType.SUBSCRIBE, queueName, User);
            String xml = xmlProvider.Serialize(packet);
            queueProvider.Push(DestinationServer.ServerQueue.QueueA, xml);
        }
        
        public virtual void CreateCommand(String queueName)
        {
            if (queueName == null)
            {
                throw new ArgumentNullException("Parametar ne sme biti null.");
            }
            if (queueName == "")
            {
                throw new ArgumentException("Parametar ne sme biti prazan string.");
            }
            Packet packet = PacketCreate(RequestType.CREATE, queueName, User);
            String xml = xmlProvider.Serialize(packet);
            queueProvider.Push(DestinationServer.ServerQueue.QueueA, xml);
        }

        public virtual void UpdateCommand()
        {
            // queueProvider.Push PORUKE NA CLIENTQUEUE
            Packet packet = PacketCreate(RequestType.UPDATE, "update", User);
            String xml = xmlProvider.Serialize(packet);
            queueProvider.Push(DestinationServer.ClientQueues[User.SubscribedTo].QueueA, xml);
        }
        #endregion

        #region Listeners
        private void ListeningOnServerQueue()
        {
            // ocekivane poruke su tipa CREATE ili SUBSCRIBE
            while (true)
            {
                if (DestinationServer.ServerQueue.QueueB.Count > 0)
                {
                    String xmlPacket;
                    xmlPacket = queueProvider.Pop(DestinationServer.ServerQueue.QueueB);
                    //send to method dexmlProvider.Serialize
                    Packet recievedPacket = xmlProvider.Deserialize(xmlPacket);
                    Console.WriteLine($"Recieved message: {recievedPacket.Message}");
                    
                    if (!String.IsNullOrEmpty(recievedPacket.User.SubscribedTo))
                    {
                        this.User.SubscribedTo = recievedPacket.User.SubscribedTo;
                        Task.Factory.StartNew(() => ListeningOnClientQueue(DestinationServer.ClientQueues[User.SubscribedTo].QueueB));
                    }
                }
                Thread.Sleep(5000);
            }
        }
        public void ListeningOnClientQueue(ConcurrentQueue<String> queue)
        {
            String xml;
            // ocekivane poruke su tipa UPDATE
            while (true)
            {
                if (queue.Count > 0 /*&& packet.User.Equals(this.User)*/)
                {
                    queue.TryPeek(out xml);
                    Packet packet = xmlProvider.Deserialize(xml);

                    if (packet.User.Equals(this.User) || this.User.UserId == 0)
                    {
                        String xmlPacket;
                        xmlPacket = queueProvider.Pop(queue);
                        //send to method dexmlProvider.Serialize
                        Packet recievedPacket = xmlProvider.Deserialize(xmlPacket);
                        Console.WriteLine($"Recieved message: {recievedPacket.Message}");
                    }
                }
                Thread.Sleep(5000);
            }

        }
        #endregion

        #region PacketCreate
        public Packet PacketCreate(RequestType command, String message, User u)
        {
            if(message == null || u == null)
            {
                throw new ArgumentNullException("Message and user can not be null.");
            }
            if(message == "")
            {
                throw new ArgumentException("Message can not be empty.");
            }
            return new Packet(message, command, u);
        }
        #endregion
    }
}
