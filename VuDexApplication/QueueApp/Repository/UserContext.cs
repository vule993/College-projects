using QueueApp.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueApp.Repository
{
    public class UserContext : DbContext
    {
        public UserContext():base()
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
        public virtual DbSet<User> Users { get; set; }
    }
}
