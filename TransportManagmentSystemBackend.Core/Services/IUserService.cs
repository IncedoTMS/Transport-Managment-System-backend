using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;

namespace TransportManagmentSystemBackend.Core.Services
{
    public interface IUserService
    {
        Task<ActionResult<IEnumerable<UserRequest>>> AddUser(UserRequest request);
        Task<ActionResult<IEnumerable<UserRequest>>> GetUser();
        Task<ActionResult<IEnumerable<UserRequest>>> DelUser(int empid);
        Task<ActionResult<IEnumerable<UserRequest>>> EditUser(int empid, UserRequest request);
    }
}
