using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        bool isValidEmail(string mail)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]+[.+-_]{0,1}[a-zA-Z0-9]+@incedoinc.com$");
            return regex.IsMatch(mail);
        }

        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
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
                else if (string.IsNullOrEmpty(request.FirstName))
                {
                    return this.BadRequest("Firstname can not be empty.");
                }
                else if (request.EmpCode == null)
                {
                    return this.BadRequest("EmpCode can not be empty.");
                }
                else if (request.Phone.Length != 10)
                {
                    return this.BadRequest("Phone number should be 10 digit number.");
                }
                else if (!isValidEmail(request.Email))
                {
                    return this.BadRequest("Please enter a valid email.");
                }
                else if (request.Password.Length < 8)
                {
                    return this.BadRequest("Password should not be less than 8 character.");
                }
                else if (string.IsNullOrEmpty(request.Manager))
                {
                    return this.BadRequest("Manager can not be empty.");
                }
                else if (string.IsNullOrEmpty(request.Office))
                {
                    return this.BadRequest("Office can not be empty.");
                }
                else if (string.IsNullOrEmpty(request.Department))
                {
                    return this.BadRequest("Department can not be empty.");
                }
                else if (string.IsNullOrEmpty(request.AddressDetails))
                {
                    return this.BadRequest("AddressDetails Field is empty.");
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
        [Authorize(Roles = "Admin")]
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

        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<UserResponse>> GetAsync(int id)
        {
            Logger.Info($"UserController.GetAsync method called.");
            try
            {
                var resp = await userService.GetUserbyId(id);
                if (resp.Id < 1)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(resp);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in UserController.GetAsync method ={ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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

        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        [Route("(EmpCode,Name,Email)")]
        public async Task<ActionResult<UserResponse>> GetUserAsync(int? EmpCode, string Name, string Email)
        {
            Logger.Info($"UserController.GetUserAsync method called.");
            try
            {
                return Ok(await userService.GetUsersDetails(EmpCode, Name, Email));
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception ocuurs in UserController.GetUserAsync method ={ex.Message}");
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
                else if (string.IsNullOrEmpty(request.UserName))
                {
                    return this.BadRequest("Username can not be empty.");
                }
                else if (!isValidEmail(request.UserName))
                {
                    return this.BadRequest("Please enter a valid username.");
                }
                else if (request.Password.Length < 8)
                {
                    return this.BadRequest("Password can not be less than 8 character.");
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
