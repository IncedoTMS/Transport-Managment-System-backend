using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;


namespace TransportManagmentSystemBackend.Core.Interfaces.Repositories
{
    public interface ICabRequirmentRequestRepository
    {

        Task<CabRequirementRequestResponse> InsertCabRequirmentRequest(CabRequirementRequest requirementRequest);
    }
}
