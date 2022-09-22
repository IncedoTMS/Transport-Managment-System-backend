using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
    [Route("api/v{version:apiVersion}/cabrequirment")]
    public class CabRequirmentController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ICabRequirementRequestService cabRequirementRequestService;

        public CabRequirmentController(ICabRequirementRequestService cabRequirementRequestService)
        {
            this.cabRequirementRequestService = cabRequirementRequestService ?? throw new ArgumentNullException(nameof(cabRequirementRequestService));
        }

        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<CabRequirementRequestResponse>> PostAsync([FromBody] Core.Domain.Models.CabRequirementRequest requirmentRequest)
        {
            Logger.Info($"CabRequirmentController.PostAsync method called.");
            Logger.Info($"CabRequirementRequest Body is {Newtonsoft.Json.JsonConvert.SerializeObject(requirmentRequest)}");
            try
            {
                if (requirmentRequest == null)
                    return BadRequest();

                var createdCabRequirmentRequest = await cabRequirementRequestService.Add(requirmentRequest);
                return createdCabRequirmentRequest == null ? NotFound() : Ok(createdCabRequirmentRequest);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in CabRequirmentController.PostAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<ActionResult<CabRequirementRequestResponse>> GetAsync()
        {
            try
            {
                return Ok(await cabRequirementRequestService.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Error retrieving data from the database");
            }

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CabRequirementRequestResponse>> Put([FromBody] Core.Domain.Models.CabRequirementRequest requirmentRequest, int id)
        {
            Logger.Info($"CabRequirmentController.PutAsync method called.");
            Logger.Info($"CabRequirementRequest Body is {Newtonsoft.Json.JsonConvert.SerializeObject(requirmentRequest)}");

            try
            {
                if (id == null || requirmentRequest == null)
                {
                    return this.BadRequest(nameof(id));
                }


                var createdCabRequirmentRequest = await cabRequirementRequestService.Update(requirmentRequest, id);
                return createdCabRequirmentRequest == null ? NotFound() : Ok(createdCabRequirmentRequest);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in CabRequirmentController.PutAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("(Id,UserId,RoleId,ManagerId)")]
        public async Task<ActionResult<CabRequirementRequestResponse>> GetCAsync(int? Id,int? UserID,int? RoleId, int? ManagerId)
        {
            try
            {
                if (Id == null && UserID == null && RoleId == null && ManagerId == null)
                {
                    return this.BadRequest(" ");
                }
                var resp = await cabRequirementRequestService.GetCabRequest(Id,UserID,RoleId,ManagerId);
                return resp == null ? NotFound() : Ok(resp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            Logger.Info($"CabRequirmentController.DeleteAsync method called.");
            try
            {
                if (id == null)
                {
                    return false;
                }

                var resp = await cabRequirementRequestService.DeleteCab(id);
                return resp == false ? false : true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in CabRequirmentController.DeleteAsync method ={ex.Message}");
                return false;
            }
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> PatchAsync([FromBody] JsonPatchDocument requirmentRequest,int id) 
        {
            Logger.Info($"CabRequirmentController.PatchAsync method called.");
            Logger.Info($"CabRequirementRequest Body is {Newtonsoft.Json.JsonConvert.SerializeObject(requirmentRequest)}");

            try
            {
                if (id == null || requirmentRequest == null)
                {
                    return this.BadRequest(nameof(id));
                }


                var createdCabRequirmentRequest = await cabRequirementRequestService.UpdatePatch(requirmentRequest, id);
                return createdCabRequirmentRequest == null ? NotFound() : Ok(createdCabRequirmentRequest);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in CabRequirmentController.PutAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }







    }
}
