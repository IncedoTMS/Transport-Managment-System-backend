using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Core.Services;

namespace TransportManagmentSystemBackend.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/cab")]
    public class CabRequirmentController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ICabService cabService;

        public CabRequirmentController(ICabService cabService)
        {
            this.cabService = cabService ?? throw new ArgumentNullException(nameof(cabService));
        }

        [ProducesResponseType(typeof(CabResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CabResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CabResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CabResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CabResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<CabResponse>> PostAsync([FromBody] CabRequest request)
        {
            Logger.Info($"CabRequestController.PostAsync method called.");
            Logger.Info($"CabRequest Body is {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
            try
            {
                if (request == null)
                {
                    return this.BadRequest(nameof(request));
                }
                
                var resp = await cabService.AddCab(request);
                return resp == null ? NotFound() : Ok(resp);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in CabRequestController.PostAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
       
        [HttpPut]
        public async Task<ActionResult<CabResponse>> PutAsync([FromBody] CabRequest request)
        {
            Logger.Info($"CabRequestController.PutAsync method called.");
            Logger.Info($"CabRequest Body is {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
            try
            {
                if (request == null)
                {
                    return this.BadRequest(nameof(request));
                }

                var resp = await cabService.UpdateCab(request);
                return resp == null ? NotFound() : Ok(resp);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in CabRequestController.PutAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }





    }
}
