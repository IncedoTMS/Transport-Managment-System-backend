using System;
using System.Collections.Generic;

namespace TransportManagmentSystemBackend.Infrastructure.Data.Entities
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
