using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagmentSystemBackend.Core.Domain.Models
{
    public class ManagerResponse
    {
        public string? ManagerName { get; set; }
        public int? ManagerId { get; set; }
        public string? ManagerEmail { get; set; }
    }
}
