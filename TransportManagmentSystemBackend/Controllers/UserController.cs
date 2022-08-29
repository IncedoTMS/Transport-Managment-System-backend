using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;
using TransportManagmentSystemBackend.Core.Services;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace TransportManagmentSystemBackend.Api.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IUserService userService;
        private readonly IJWTService _jWTManager;


        public UserController(IUserService userService, IJWTService jWTManager)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this._jWTManager = jWTManager;
        }

        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<UserResponse>> PostAsync([FromBody] UserRequest request)
        {
            Logger.Info($"UserController.PostAsync method called.");
            Logger.Info($"UserRequest Body is {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
            try
            {
                if (request == null)
                {
                    return this.BadRequest(nameof(request));
                }

                var resp = await userService.AddUser(request);
                return resp == null ? NotFound() : Ok(resp);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in UserController.PostAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> PutAsync(int id, UserRequest request)
        {
            Logger.Info($"UserController.PutAsync method called.");
            Logger.Info($"UserRequest Body is {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");

            try
            {
                if (id == null || request == null)
                {
                    return this.BadRequest(nameof(id));
                }

                var resp = await userService.UpdateUser(id, request);
                return resp == null ? NotFound() : Ok(resp);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in UserController.PutAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetAsync()
        {
            Logger.Info($"UserController.GetAsync method called.");
            try
            {
                return Ok(await userService.GetUsers());
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in UserController.GetAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserResponse>> DeleteAsync(int id)
        {
            Logger.Info($"UserController.DeleteAsync method called.");
            try
            {
                if (id == null)
                {
                    return this.BadRequest(nameof(id));
                }

                var resp = await userService.DeleteUser(id);
                return resp == null ? NotFound() : Ok(resp);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in UserController.DeleteAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authentication(UserLoginRequest usersdata)
        {
            var token = _jWTManager.Authentication(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> LoginPostAsync([FromBody] UserLoginRequest request)
        {
            Logger.Info($"UserController.LoginPostAsync method called.");
            Logger.Info($"UserLoginRequest Body is {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");

            try
            {
                if (request == null)
                {
                    return this.BadRequest(nameof(request));
                }

                var resp = await userService.GetUserLogin(request);
                return resp == null ? NotFound() : Ok(resp);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in UserController.LoginPostAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
