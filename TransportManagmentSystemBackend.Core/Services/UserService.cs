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
        public async Task<UserResponse> UpdateUser(UserRequest request)
        {
            if (request == null)
            {
                Logger.Error("UserService.UpdateUser is called and getting null exception for update user");
                throw new ArgumentException(nameof(UpdateUser));
            }
            else
            {
                return await _repo.UpdateUser(request);
            }
        }
        public async Task<UserResponse> GetUser(UserRequest request)
        {
            if (request == null)
            {
                Logger.Error("UserService.GetUser is called and getting null exception for get user");
                throw new ArgumentException(nameof(GetUser));
            }
            else
            {
                return await _repo.GetUser(request);
            }
        }
    }
}
