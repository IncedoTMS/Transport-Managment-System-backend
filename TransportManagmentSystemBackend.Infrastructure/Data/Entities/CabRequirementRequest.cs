using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransportManagementSystemBackend.Infrastructure.Data.Entities
{
    public class CabRequirementRequest
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TimeSlotId { get; set; }
        public DateTime? RequestDate { get; set; }
        public bool? IsApproved { get; set; }
        public string ApprovedBy { get; set; }
        public string PickUpLocation { get; set; }
        public string DropLocation { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual CabRequirementSlot TimeSlot { get; set; }
        public virtual User User { get; set; }
    }
}
