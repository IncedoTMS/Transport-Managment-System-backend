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
    }
}
