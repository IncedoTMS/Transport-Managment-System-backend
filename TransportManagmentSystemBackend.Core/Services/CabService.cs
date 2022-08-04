using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;

namespace TransportManagmentSystemBackend.Core.Services
{
    public class CabService : ICabService
    {
        private readonly ICabRepository _repo;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public CabService(ICabRepository repo)
        {
            this._repo = repo;
        }

        public async Task<CabResponse> AddCab(CabRequest request)
        {
            if (request == null)
            {
                Logger.Error("CabService.AddCab is called and getting null exception for add cab");
                throw new ArgumentException(nameof(AddCab));
            }
            else
            {
                return await _repo.InsertCab(request);
            }
        }
        public async Task<CabResponse> UpdateCab(CabRequest request)
        {
            if (request == null)
            {
                Logger.Error("CabService.UpdateCab is called and getting null exception for update cab");
                throw new ArgumentException(nameof(UpdateCab));
            }
            else
            {
                return await _repo.UpdateCab(request);
            }
        }
    }
}
