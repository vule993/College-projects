using QueueApp.Common;
using QueueApp.Model;
using QueueApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueApp.View
{
    public class UserView : IUserView
    {
        public void DisplayError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine($"Error {error}");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Environment.NewLine);
        }
        public void DisplayOperations()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nDostupne operacije:");
            Console.WriteLine("\t1. Napravi novi par redova");
            Console.WriteLine("\t2. Pretplati se na postojeci");
            Console.WriteLine("\t3. Povratak na glavni meni");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\t>> ");
        }

        public void DisplayEditOperations()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nDostupne operacije:");
            Console.WriteLine("\t1. Azuriraj postojece stanje");
            Console.WriteLine("\t2. Povratak na glavni meni");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\t>> ");
        }

        public void DisplayAllQueues(Server server)
        {
            String output = "==========================================\n";
            output += "\tTitle-ovi dostupnih redova\n\n";

            foreach (String pair in server.ClientQueues.Keys)
            {
                output += "Queue title: " + pair + "\n";
            }

            output += "\n==========================================\n";

            Console.WriteLine(output);
        }

        public void DisplayWait()
        {
            Console.WriteLine($"");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Please wait...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"");
        }
        public void DisplayContinue()
        {
            Console.WriteLine($"");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"You can continue now...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"");
        }

        public void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Glavni meni:");
            Console.WriteLine("\t1. Izaberi nekog postojeceg User-a");
            Console.WriteLine("\t2. Unos novog User-a");
            Console.WriteLine("\t3. Izlaz");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\t>> ");
        }





        public void DisplayList(List<User> list)
        {
            String output = "";
            output += "\n";
            output += "==========================";
            foreach(User u in list)
            {
                output += u;
            }
            output += "==========================";
            output += "\n";
            Console.WriteLine(output);
        }
        public void DisplayAllUsers(Server server)
        {
            String output = "==========================================\n";
            output += "\tID-evi dostupnih klijenata\n\n";

            foreach (KeyValuePair<String, ClientQueue> pair in server.ClientQueues)
            {
                foreach (var element in pair.Value.SubscribedClients)
                {
                    output += "User ID: " + element.UserId + "\n";
                }
            }

            output += "\n==========================================\n";

            Console.WriteLine(output);
        }
        public void DisplayUser(User user)
        {
            Console.WriteLine(user.ToString());
        }
    }
}
