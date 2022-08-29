using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;

namespace TransportManagmentSystemBackend.Core.Services
{
    public class CabRequirementRequestService : ICabRequirementRequestService
    {
        private readonly ICabRequirmentRequestRepository _repo;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public CabRequirementRequestService(ICabRequirmentRequestRepository repo)
        {
            this._repo = repo;
        }

        public async Task<CabRequirementRequestResponse> Add(CabRequirementRequest request)
        {
            if (request == null)
            {
                Logger.Error("CabRequirementRequestService.Add is called and getting null exception for add cab");
                throw new ArgumentException(nameof(Add));
            }
            else
            {
                return await _repo.InsertCabRequirmentRequest(request);
            }
        }
        public async Task<List<CabRequirementRequestResponse>> GetAll()
        {
            
            
            {
                return await _repo.GetCab();
            }
        }
        public async Task<CabRequirementRequestResponse> GetCabRequest(int Id)
        {


            {
                return await _repo.GetCabById(Id);
            }
        }
        public async Task<CabRequirementRequestResponse> Update(CabRequirementRequest request,int Id)
        {
            if (request == null)
            {
                Logger.Error("CabRequirementRequestService.Update is called and getting null exception for update cab");
                throw new ArgumentException(nameof(Update));
            }
            else
            {
                return await _repo.UpdateCabRequirmentRequest(request,Id);
            }
        }
        public async Task<bool> DeleteCab(int id)
        {
            if (id == null)
            {
                Logger.Error("UserService.DeleteUser is called and getting null exception for delete user");
                throw new ArgumentException(nameof(DeleteCab));
            }
            else
            {
                return await _repo.DeleteThisCab(id);
            }
        }

    }
}
