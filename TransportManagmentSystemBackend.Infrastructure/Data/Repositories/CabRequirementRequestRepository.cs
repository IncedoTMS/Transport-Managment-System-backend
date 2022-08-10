using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementSystemBackend.Infrastructure.Data.Contexts;
using TransportManagementSystemBackend.Infrastructure.Data.Entities;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;

namespace TransportManagmentSystemBackend.Infrastructure.Data.Repositories
{
    public class CabRequirementRequestRepository : ICabRequirmentRequestRepository
    {
        private readonly IMapper _mapper;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly TMSContext appDbContext;

        public CabRequirementRequestRepository(TMSContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CabRequirementRequestResponse> InsertCabRequirmentRequest(Core.Domain.Models.CabRequirementRequest request)
        {
            try
            {
                var response = new CabRequirementRequestResponse();

                appDbContext.CabRequirementRequests.Add(new TransportManagementSystemBackend.Infrastructure.Data.Entities.CabRequirementRequest
                {
                    UserId = request.UserId,
                    TimeSlotId = request.TimeSlotId,
                    RequestDate = request.RequestDate,
                    IsApproved = false,
                    ApprovedBy = "SystemUser",
                    PickUpLocation = request.PickUpLocation,
                    DropLocation = request.DropLocation,
                    IsDeleted = false,
                    CreatedBy = "SystemUser",
                    CreatedDate = DateTime.Now
                });

                int id = appDbContext.SaveChanges();

                response.Id = id;
                response.UserId = request.UserId;
                response.TimeSlotId = request.TimeSlotId;
                response.RequestDate = request.RequestDate;
                response.IsApproved = request.IsApproved;
                response.PickUpLocation = request.PickUpLocation;
                response.DropLocation = request.DropLocation;
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in InsertRecord For CabRequirmentRequest is {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

    }
}

