using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementSystemBackend.Infrastructure.Data.Contexts;
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
                    IsApproved = request.IsApproved,
                    ApprovedBy = "SystemUser",
                    PickUpLocation = request.PickUpLocation,
                    DropLocation = request.DropLocation,
                    IsDeleted = false,
                    CreatedBy = "SystemUser",
                    CreatedDate = DateTime.Now
                }); ;

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
        public async Task<List<CabRequirementRequestResponse>> GetCab()
        {
            try
            {
                return await appDbContext.CabRequirementRequests.Select(x => new CabRequirementRequestResponse
                {
                    Id = x.Id,
                    UserId = x.UserId,

                    TimeSlotId = x.TimeSlotId,
                    RequestDate = (DateTime)x.RequestDate,
                    IsApproved = x.IsApproved,
                    PickUpLocation = x.PickUpLocation,
                    DropLocation = x.DropLocation,
                }).ToListAsync();

            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in GetRecord For CabRequirmentRequest is {ex.Message}");
                throw new Exception(ex.Message);

            }
        }
        public async Task<CabRequirementRequestResponse> UpdateCabRequirmentRequest(Core.Domain.Models.CabRequirementRequest request, int Id)
        {
            var response = new CabRequirementRequestResponse();
            try
            {
                var cab = await appDbContext.CabRequirementRequests.FindAsync(Id);
                if (cab != null)
                {
                    
                    cab.UserId = request.UserId;
                    cab.TimeSlotId = request.TimeSlotId;
                    cab.RequestDate = request.RequestDate;
                    cab.IsApproved = request.IsApproved;
                    cab.PickUpLocation = request.PickUpLocation;
                    cab.DropLocation = request.DropLocation;
                    await appDbContext.SaveChangesAsync();
                    response.Id = cab.Id;
                    response.UserId = request.UserId;
                    response.TimeSlotId = request.TimeSlotId;
                    response.RequestDate = request.RequestDate;
                    response.IsApproved = request.IsApproved;
                    response.PickUpLocation = request.PickUpLocation;
                    response.DropLocation = request.DropLocation;

                }
                else
                {
                    return null;
                }
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in UpdateRecord For CabRequirmentRequest is {ex.Message}");
                throw new Exception(ex.Message);

            }
        }
        public async Task<CabRequirementRequestResponse> GetCabById(int Id)
        {
            var response = new CabRequirementRequestResponse();
            try
            {
                var res = await appDbContext.CabRequirementRequests.FirstOrDefaultAsync(e => e.Id == Id);
                if (res != null)
                {
                    response.Id = res.Id;
                    response.UserId = res.UserId;
                    response.TimeSlotId = res.TimeSlotId;
                    response.RequestDate = (DateTime)res.RequestDate;
                    response.IsApproved = res.IsApproved;
                    response.PickUpLocation = res.PickUpLocation;
                    response.DropLocation = res.DropLocation;

                }
                else
                {
                    return null;
                }
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in GetRecord For CabRequirmentRequest is {ex.Message}");
                throw new Exception(ex.Message);

            }


        }
        public async Task<bool> DeleteThisCab(int id)
        {
            var response = new CabRequirementRequestResponse();
            try
            {
                var res = await appDbContext.CabRequirementRequests.FindAsync(id);

                if (res != null)
                {
                    response.Id = res.Id;
                    response.UserId = res.UserId;
                    response.TimeSlotId = res.TimeSlotId;
                    response.RequestDate = (DateTime)res.RequestDate;
                    response.IsApproved = res.IsApproved;
                    response.PickUpLocation = res.PickUpLocation;
                    response.DropLocation = res.DropLocation;


                    appDbContext.CabRequirementRequests.Remove(res);

                    await appDbContext.SaveChangesAsync();
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;

            }
        }
        public async Task<CabRequirementRequestResponse> UpdatePatchCabRequirmentRequest(JsonPatchDocument request, int Id)
        {
            var response = new CabRequirementRequestResponse();
            try
            {
                var cab = await appDbContext.CabRequirementRequests.FindAsync(Id);
                if (cab != null)
                {

                  request.ApplyTo(cab);
                    await appDbContext.SaveChangesAsync();
                    response.UserId = cab.UserId;
                    response.Id = cab.Id;
                    response.TimeSlotId = cab.TimeSlotId;
                    response.RequestDate = (DateTime)cab.RequestDate;
                    response.IsApproved = cab.IsApproved;
                    response.PickUpLocation = cab.PickUpLocation;
                    response.DropLocation = cab.DropLocation;


                }
                else
                {
                    return null;
                }
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in UpdatePatchRecord For CabRequirmentRequest is {ex.Message}");
                throw new Exception(ex.Message);

            }
        }



    }
}




            
        
        