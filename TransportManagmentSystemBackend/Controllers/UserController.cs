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
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
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
