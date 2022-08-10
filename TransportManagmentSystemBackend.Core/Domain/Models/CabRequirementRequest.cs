using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagmentSystemBackend.Core.Domain.Models
{
    public class CabRequirementRequest
    {
        public int? UserId { get; set; }
        public int? TimeSlotId { get; set; }
        public DateTime? RequestDate { get; set; }
        public bool IsApproved { get; set; }
        public string PickUpLocation { get; set; }
        public string DropLocation { get; set; }
    }
}
