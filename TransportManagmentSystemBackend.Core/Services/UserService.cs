using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;

namespace TransportManagmentSystemBackend.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public UserService(IUserRepository repo)
        {
            this._repo = repo;
        }

        public async Task<UserResponse> AddUser(UserRequest request)
        {
            if (request == null)
            {
                Logger.Error("UserService.AddUser is called and getting null exception for add user");
                throw new ArgumentException(nameof(AddUser));
            }
            else
            {
                return await _repo.InsertUser(request);
            }
        }

        public async Task<UserResponse> UpdateUser(int id, UserRequest request)
        {
            if (id == null)
            {
                Logger.Error("UserService.AddUser is called and getting null exception for edit user");
                throw new ArgumentException(nameof(UpdateUser));
            }
            else
            {
                return await _repo.UpdateThisUser(id, request);
            }
        }
        public async Task<List<UserResponse>> GetUsers()
        {
                return await _repo.GetAllUsers();
        }

        public async Task<UserResponse> DeleteUser(int id)
        {
            if (id == null)
            {
                Logger.Error("UserService.DeleteUser is called and getting null exception for delete user");
                throw new ArgumentException(nameof(DeleteUser));
            }
            else
            {
                return await _repo.DeleteThisUser(id);
            }
        }
        public async Task<UserResponse> GetUser(int? Id, int? EmpCode, string? Email)
        {
            return await _repo.GetUserData(Id, EmpCode, Email);
        }

        public async Task<UserLoginResponse> GetUserLogin(UserLoginRequest request)
        {
            if (request == null)
            {
                Logger.Error("UserService.GetUserLogin is called and getting null exception for add user");
                throw new ArgumentException(nameof(GetUserLogin));
            }
            else
            {
                return await _repo.GetUserDetails(request);
            }
        }
    }
}
