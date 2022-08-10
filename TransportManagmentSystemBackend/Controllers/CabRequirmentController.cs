using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportManagementSystemBackend.Infrastructure.Data.Entities;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;
using TransportManagmentSystemBackend.Core.Services;
using TransportManagmentSystemBackend.Infrastructure.Data.Repositories;

namespace TransportManagmentSystemBackend.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/cab")]
    public class CabRequirmentController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ICabRequirmentRequestRepository cabRepository;

        public CabRequirmentController(ICabRequirmentRequestRepository cabRepository)
        {
            this.cabRepository = cabRepository;
        }
        //[ProducesResponseType(typeof(CabRequirementRequest), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(CabRequirementRequest), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(CabRequirementRequest), StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(CabRequirementRequest), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(CabRequirementRequest), StatusCodes.Status500InternalServerError)]

        //[HttpGet]
        //public async Task<ActionResult> GetCab()
        //{
        //    try
        //    {
        //        return Ok(await cabRepository.GetCab());
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error retrieving data from the database");
        //    }
        //}
        //[HttpPost]
        //public async Task<ActionResult<CabRequirementRequest>> AddCab(CabRequirementRequest requirmentRequest)
        //{
        //    try
        //    {
        //        if (requirmentRequest == null)
        //            return BadRequest();

        //        var createdCabRequirmentRequest = await cabRepository.Insert(requirmentRequest);
        //        return createdCabRequirmentRequest == null? NotFound(): Ok(createdCabRequirmentRequest);
        //        //return CreatedAtAction(nameof(GetCab),
        //           // new { id = createdCabRequirmentRequest.Id }, createdCabRequirmentRequest);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error creating new  record");
        //    }
        //}
        


    }
}
