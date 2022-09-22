using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagmentSystemBackend.Core.Domain.Models
{
    public class ChangePasswordResponse
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
        public string? Password { get; set; }
    }
}
