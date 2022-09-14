using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagmentSystemBackend.Core.Domain.Models
{
    public class UserLoginResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? EmpCode { get; set; }
        public string? UserName { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public int? AddressId { get; set; }
        public string Department { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Manager { get; set; }
        public string Office { get; set; }
    }
}
