using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagmentSystemBackend.Core.Domain.Models
{
    public class CabRequirementRequestResponse : CabRequirementRequest
    {
        public int Id { get; set; }
        public int EmpCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
