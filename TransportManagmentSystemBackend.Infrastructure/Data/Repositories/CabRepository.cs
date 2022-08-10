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
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;

namespace TransportManagmentSystemBackend.Infrastructure.Data.Repositories
{
    public class CabRepository : ICabRequirmentRequestRepository
    {
        private readonly IMapper _mapper;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly TMSContext appDbContext;
        public CabRepository(TMSContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        //public async Task<CabRequirementRequest> UpdateCab(CabRequirementRequest requirementRequest)
        //{
        //    var result = await appDbContext.CabRequirementRequests
        //        .FirstOrDefaultAsync(e => e.Id == requirementRequest.Id);

        //    if (result != null)
        //    {
        //        result.UserId = requirementRequest.UserId;
        //        result.TimeSlotId = requirementRequest.TimeSlotId;
        //        //result.TimeSlot = requirementRequest.TimeSlot;
        //        result.ApprovedBy = requirementRequest.ApprovedBy;
        //        result.CreatedBy = requirementRequest.CreatedBy;
        //        result.PickUpLocation = requirementRequest.PickUpLocation;
        //        result.DropLocation = requirementRequest.DropLocation;
        //        result.CreatedDate = requirementRequest.CreatedDate;
        //        //result.CreatedBy = requirementRequest.CreatedBy;
        //        result.IsDeleted = requirementRequest.IsDeleted;
        //        result.IsApproved = requirementRequest.IsApproved;
        //        result.RequestDate = requirementRequest.RequestDate;
                



                

        //        await appDbContext.SaveChangesAsync();

        //        return result;
        //    }

        //    return null;
        //}

        //public async Task<IEnumerable<CabRequirementRequest>> GetCab()
        //{
        //    return await appDbContext.CabRequirementRequests.ToListAsync();
        //}

        //public async Task<CabRequirementRequest> InsertUser(CabRequirementRequest requirementRequest)
        //{
        //    try
        //    {
        //        var result = await appDbContext.CabRequirementRequests.AddAsync(requirementRequest);
        //        appDbContext.CabRequirementRequests.Add(requirementRequest);
        //        await appDbContext.SaveChangesAsync();
        //        return result.Entity;
        //        //return await appDbContext.CabRequirementRequests.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    //return null;
        //}
        public async Task<CabRequirementRequest> GetCab(int Id)
        {
            return await appDbContext.CabRequirementRequests
                .FirstOrDefaultAsync(e => e.Id == Id);
        }

    }
}

