using Microsoft.AspNetCore.Mvc;
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
        Task<List<CabRequirementRequestResponse>> GetCab();
        Task<CabRequirementRequestResponse> GetCabById(int Id);
        Task<CabRequirementRequestResponse> UpdateCabRequirmentRequest(CabRequirementRequest requirementRequest,int Id);
        
    }
}
