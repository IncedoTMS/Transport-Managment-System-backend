using System;
using System.Collections.Generic;

namespace TransportManagementSystemBackend.Infrastructure.Data.Entities
{
    public partial class CabRequirementSlot
    {
        public CabRequirementSlot()
        {
            CabRequirementRequests = new HashSet<CabRequirementRequest>();
        }

        public int Id { get; set; }
        public byte[] Time { get; set; }
        public int SlotAvailableTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<CabRequirementRequest> CabRequirementRequests { get; set; }
    }
}
