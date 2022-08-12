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
        Task<UserResponse> AddUser(UserRequest request);
        Task<UserResponse> EditUser(int id, UserRequest request);
    }
}
