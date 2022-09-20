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
        Task<UserResponse> UpdateUser(int id, UserRequest request);
        Task<UserResponse> GetUserbyId(int Id);
        Task<UserResponse> DeleteUser(int id);
        Task<List<UserResponse>> GetUsersDetails(int? EmpCode, string Name, string Email, int? ManagerId);
        Task<UserLoginResponse> GetUserLogin(UserLoginRequest request);
        Task<List<ManagerResponse>> GetManagersDetails();
    }
}
