using System;
using System.Collections.Generic;

namespace TransportManagementSystemBackend.Infrastructure.Data.Entities
{
    public partial class Manager
    {
        public Manager()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmail { get; set; }
        public string Password { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
