using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;


namespace TransportManagmentSystemBackend.Core.Services
{
    public interface ICabRequirementRequestService
    {
        Task<CabRequirementRequestResponse> Add(CabRequirementRequest request);
        Task<List<CabRequirementRequestResponse>> GetAll();
        Task<CabRequirementRequestResponse> Update(CabRequirementRequest request,int Id);
        Task<IQueryable<CabRequirementRequestResponse>> GetCabRequest(int? Id,int? UserId,int? RoleId);
        Task<bool> UpdatePatch(JsonPatchDocument request, int Id);
        
        Task<bool> DeleteCab(int id);

    }
}
