using Microsoft.AspNetCore.JsonPatch;
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
        Task<List<CabRequirementRequestResponse>> GetCabById(int? Id,int? UserId,int? RoleId);
        Task<CabRequirementRequestResponse> UpdateCabRequirmentRequest(CabRequirementRequest requirementRequest,int Id);
        Task<bool> UpdatePatchCabRequirmentRequest(JsonPatchDocument requirementRequest, int Id);

        Task<bool> DeleteThisCab(int id);
    }
}
