using QueueApp.Common;
using QueueApp.Model;
using QueueApp.Presenter;
using QueueApp.Repository;
using QueueApp.Service;
using QueueApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Server server = new Server();

            UserContext userContext = new UserContext();    //inicijalizacija baze?
            IRepository repo = new Repository(server, userContext);   //da li se server na ovaj naci povezuje sa repo?

            IUserView userView = new UserView();    //ovde cemo view koristiti da vrati ispis i to cemo slati kao rez string
            User user = new User();
            IUserModel userModel = new UserModel(server, user);                   //nad prosledjenim repo-m sprovode se operacije dostupne u userModelu
            IUserPresenter userPresenter = new UserPresenter(userView, userModel);


            String input = "";
            String input1 = "";
            String queueName = "";

            do
            {
                //Console.WriteLine("Glavni meni:");
                //Console.WriteLine("\t1. Izaberi nekog postojeceg User-a");
                //Console.WriteLine("\t2. Unos novog User-a");
                //Console.WriteLine("\t3. Izlaz");
                //Console.Write("\t>> ");
                userView.DisplayMenu();
                input = Console.ReadLine();
                //Console.WriteLine(Environment.NewLine);

                switch (input)
                {
                    case "1":

                        if(CountUsers(server) == 0)
                        {
                            userView.DisplayError("Nema korisnika koje mozete izabrati. Pokusajte sa opcijom 2.");
                            
                            break;
                        }

                        user = ChooseUser(server, userView);  //metoda iz main-a

                        if(user == null)
                        {
                            break;
                        }


                        if (!user.IsListening)
                        {
                            userPresenter.ListeningOnClientQueue(server.ClientQueues[user.SubscribedTo].QueueB);
                            user.IsListening = true;
                        }// ako klijent ne slusa na svom redu, pocni da slusas




                        ////////
                        userView.DisplayEditOperations();
                     
                        ////////



                        input1 = Console.ReadLine();
                        Console.WriteLine(Environment.NewLine);

                        if (input1 == "1")
                        {
                            user = CreateOrUpdateUser(user);
                            ((UserModel)userModel).User = user;
                            ((UserPresenter)userPresenter).UserModel = userModel;
                            
                            userPresenter.UpdateCommand();
                        }

                        break;

                    case "2":
                        user = CreateOrUpdateUser();


                        userView.DisplayOperations();
                        
                        input1 = Console.ReadLine();



                        //Console.WriteLine(Environment.NewLine);

                        switch (input1)
                        {
                            case "1":
                                queueName = CreateQueue();
                                ((UserModel)userModel).User = user;
                                ((UserPresenter)userPresenter).UserModel = userModel;

                                userPresenter.CreateCommand(queueName);
                            break;

                            case "2":
                                if(CountQueues(server) == 0)
                                {
                                    userView.DisplayError("Trenutno ne postoji red na koji biste se mogli pretplatiti.");
                                    break;
                                }
                                userView.DisplayAllQueues(server);
                                queueName = ChooseQueue(server);

                                ((UserModel)userModel).User = user;
                                ((UserPresenter)userPresenter).UserModel = userModel;
                                userPresenter.SubscribeCommand(queueName);
                                break;
                        }

                        break;

                    case "3":
                        userView.DisplayWait();
                        break;

                    default:
                        userView.DisplayError("Moguce opcije su '1', '2', 'Izlaz'\n");
                        break;
                }
            } while (input != "3");
        }


        private static int CountUsers(Server server)
        {
            int noOfUsers = 0;


            foreach (KeyValuePair<String, ClientQueue> pair in server.ClientQueues)
            {
                foreach (var element in pair.Value.SubscribedClients)
                {
                    noOfUsers++;
                }
            }
            

            return noOfUsers;
        }






        private static User CreateOrUpdateUser(User u = null)
        {
            User user;
            if (u == null)
            {
                Console.WriteLine();
                Console.WriteLine("Unosenje novog user-a: ");
                Console.WriteLine();
                user = new User();
            }
            else
            {
                Console.WriteLine("Azuriranje postojeceg user-a: ");
                user = u;
            }
            

            int inputN = 0;
            bool ok;

            //input items
            do
            {
                Console.Write("Unesite broj item-a:\t");
                ok = Int32.TryParse(Console.ReadLine(), out inputN);
            } while (!ok);
            
            for(int i = 0; i < inputN; i++)
            {
                Item item = new Item();

                Console.Write("Unesite inicijalno stanje [1-active , 0-not active]:\t");
                item.Active = (Console.ReadLine() == "1") ? true : false;

                Console.Write("Unesite razornu moc Item-a:\t");
                item.DestructivePower = Double.Parse(Console.ReadLine());

                Console.Write("Unesite ime Item-a:\t");
                item.Name = Console.ReadLine();

                Console.Write("Unesite kolicinu Item-a:\t");
                item.Quantity = Double.Parse(Console.ReadLine());

                user.Items.Add(item);
            }

            //input positions
            do
            {
                Console.Write("\nUnesite broj position-a:\t");
                ok = Int32.TryParse(Console.ReadLine(), out inputN);
            } while (!ok);

            for (int i = 0; i < inputN; i++)
            {
                Position position = new Position();

                Console.Write("Unesite x koordinatu:\t");
                position.X = Double.Parse(Console.ReadLine());

                Console.Write("Unesite y koordinatu:\t");
                position.Y = Double.Parse(Console.ReadLine());

                Console.Write("Unesite z koordinatu:\t");
                position.Z = Double.Parse(Console.ReadLine());

                user.Positions.Add(position);
            }

            return user;
        }









        private static User ChooseUser(Server server, IUserView userView)
        {
            userView.DisplayAllUsers(server);
            Console.Write("Unesite id korisnika: ");
            int id = Int32.Parse(Console.ReadLine());

            if(id < 0 || id > CountUsers(server))
            {
                userView.DisplayError($"Mozete uneti samo id-eve izmedju 0 i {CountUsers(server) + 1}");
                return null;   //ako nesto zeznemo vracamo null da tamo detektujemo
            }



            User user = null;

            foreach(var clientQueue in server.ClientQueues.Values)
            {
                user =  clientQueue.SubscribedClients.Find(x => x.UserId == id);
                if (user != null)
                    return user;
            }

            // nikad nece doci do ovog return-a
            return null;
        }
        private void UpdateUser(User u) {

        }
        private static int CountQueues(Server server)
        {
            int noOfQueues = 0;

            foreach (String pair in server.ClientQueues.Keys)
            {
                noOfQueues++;
            }

            return noOfQueues;
        }
        private static String CreateQueue()
        {
            Console.Write("Unesite naziv reda: ");
            return Console.ReadLine();
        }
        private static String ChooseQueue(Server server)
        {
            //PrintAllQueues(server);
            Console.Write("Unesite naziv reda: ");
            return Console.ReadLine();
        }
    }
}
