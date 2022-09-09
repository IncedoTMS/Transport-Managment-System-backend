using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagmentSystemBackend.Core.Domain.Enum
{
    public class CabRequestStatus
    {
        public enum CabRequirementRequestStatus
        {
            Approved = 1,
            Rejected = 2,
            Pending = 3
        };
    }
}
