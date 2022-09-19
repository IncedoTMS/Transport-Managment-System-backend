using Microsoft.AspNetCore.Authorization;
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
        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Authorize(Roles = "Admin, User, Manager")]
        public async Task<ActionResult<CabRequirementRequestResponse>> PostAsync([FromBody] Core.Domain.Models.CabRequirementRequest requirmentRequest)
        {
            Logger.Info($"CabRequirmentController.PostAsync method called.");
            Logger.Info($"CabRequirementRequest Body is {Newtonsoft.Json.JsonConvert.SerializeObject(requirmentRequest)}");
            try
            {
                if (requirmentRequest == null)
                {
                    return BadRequest();
                }

                var createdCabRequirmentRequest = await cabRequirementRequestService.Add(requirmentRequest);
                return createdCabRequirmentRequest == null ? NotFound() : Ok(createdCabRequirmentRequest);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in CabRequirmentController.PostAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType(typeof(List<CabRequirementRequestResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<CabRequirementRequestResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<CabRequirementRequestResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<CabRequirementRequestResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<CabRequirementRequestResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<CabRequirementRequestResponse>), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<List<CabRequirementRequestResponse>>> GetAsync()
        {
            try
            {
                return Ok(await cabRequirementRequestService.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CabRequirementRequestResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, User, Manager")]
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

        [ProducesResponseType(typeof(List<CabRequirementRequestResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<CabRequirementRequestResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<CabRequirementRequestResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<CabRequirementRequestResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<CabRequirementRequestResponse>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<CabRequirementRequestResponse>), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("(Id,UserId,RoleID)")]
        [Authorize(Roles = "Admin, User, Manager")]
        public async Task<ActionResult<List<CabRequirementRequestResponse>>> GetCAsync(int? Id,int? UserID,int? RoleID)
        {
            try
            {
                if (Id == null && UserID == null && RoleID == null)
                {
                    return this.BadRequest(" ");
                }

                var resp = await cabRequirementRequestService.GetCabRequest(Id,UserID,RoleID);
                return resp == null ? NotFound() : Ok(resp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
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

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status500InternalServerError)]
        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin, Manager")]
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
                return createdCabRequirmentRequest == false ? NotFound() : Ok(createdCabRequirmentRequest);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in CabRequirmentController.PutAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
