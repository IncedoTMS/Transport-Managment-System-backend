using System;
using System.Collections.Generic;

namespace TransportManagementSystemBackend.Infrastructure.Data.Entities
{
    public partial class User
    {
        public User()
        {
            CabRequirementRequests = new HashSet<CabRequirementRequest>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public int? AddressId { get; set; }
        public string Department { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int? ManagerId { get; set; }
        public string Office { get; set; }
        public string AddressDetails { get; set; }

        public virtual Address Address { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<CabRequirementRequest> CabRequirementRequests { get; set; }
    }
}
