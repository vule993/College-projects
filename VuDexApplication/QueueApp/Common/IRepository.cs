using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueApp.Common
{
    public interface IRepository
    {
        IEnumerable<User> GetAll();
        User Add(User element);
        void Update(User element);
        int Save();
    }
}
