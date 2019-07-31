using QueueApp.Model;
using QueueApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueApp.Common
{
    public interface IUserView
    {
        void DisplayMenu();
        void DisplayList(List<User> list);
        void DisplayAllUsers(Server s);
        void DisplayAllQueues(Server s);
        void DisplayWait();
        void DisplayOperations();
        void DisplayEditOperations();
        void DisplayContinue();
        void DisplayUser(User user);
        void DisplayError(String error);
    }
}
