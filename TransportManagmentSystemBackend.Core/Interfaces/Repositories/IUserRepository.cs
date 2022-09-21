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
        Task<UserResponse> InsertUser(UserRequest request);
        Task<UserResponse> UpdateThisUser(int id, UserRequest request);
        Task<UserResponse> GetUserDatabyId(int Id);
        Task<UserResponse> DeleteThisUser(int id);
        Task<List<UserResponse>> GetUsersData(int? EmpCode, string Name, string Email, int? ManagerId);
        Task<UserLoginResponse> GetUserDetails(UserLoginRequest request);
        Task<List<ManagerResponse>> GetManagersData();
        Task<ChangePasswordResponse> UpdateThisPassword(int Id, string Email, int RoleId, string Password);
    }
}
