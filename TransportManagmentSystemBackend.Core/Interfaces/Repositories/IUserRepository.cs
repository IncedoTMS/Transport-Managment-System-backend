using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;

namespace TransportManagmentSystemBackend.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<ActionResult<IEnumerable<UserRequest>>> InsertUser(UserRequest request);
        Task<ActionResult<IEnumerable<UserRequest>>> GetUsers();
        Task<ActionResult<IEnumerable<UserRequest>>> DeleteUser(int empid);
        Task<ActionResult<IEnumerable<UserRequest>>> EditThisUser(int empid, UserRequest request);
    }
}
