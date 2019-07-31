using QueueApp.Common;
using QueueApp.Model;
using QueueApp.View;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueApp.Presenter
{
    public class UserPresenter : IUserPresenter
    {
        private readonly IUserView userView;
        private IUserModel userModel;
        public IUserView UserView { get; set; }
        public IUserModel UserModel { get; set; }
        
        public UserPresenter(IUserView userView, IUserModel userModel)
        {
            this.userView = userView;
            this.userModel = userModel;
        }

        #region Commands
        public void CreateCommand(String queueName)
        {
            userModel.CreateCommand(queueName);
            userView.DisplayWait();
            Thread.Sleep(20000);
            userView.DisplayAllUsers(((UserModel)userModel).DestinationServer);
            userView.DisplayContinue();
        }
        
        public void SubscribeCommand(String queueName)
        {
            userModel.SubscribeCommand(queueName);
        }

        public void UpdateCommand()
        {
            userModel.UpdateCommand();
            userView.DisplayWait();
            Thread.Sleep(20000);
        }

        public void ListeningOnClientQueue(ConcurrentQueue<String> queue)
        {
            Task.Factory.StartNew(() => userModel.ListeningOnClientQueue(queue));
        }
        #endregion
    }
}
