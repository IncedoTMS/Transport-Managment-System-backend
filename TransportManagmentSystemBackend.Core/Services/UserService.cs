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

        public async Task<ActionResult<IEnumerable<UserRequest>>> AddUser(UserRequest request)
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
        public async Task<ActionResult<IEnumerable<UserRequest>>> GetUser()
        {
            //if (request == null)
            //{
            //    Logger.Error("UserService.AddUser is called and getting null exception for add user");
            //    throw new ArgumentException(nameof(AddUser));
            //}
            //else
            //{
                return await _repo.GetUsers();
            //}
        }
        public async Task<ActionResult<IEnumerable<UserRequest>>> DelUser(int empid)
        {
            return await _repo.DeleteUser(empid);
        }
        public async Task<ActionResult<IEnumerable<UserRequest>>> EditUser(int empid, UserRequest request)
        {
            if (request == null)
            {
                Logger.Error("UserService.EditUser is called and getting null exception for add user");
                throw new ArgumentException(nameof(EditUser));
            }
            else
            {
                return await _repo.EditThisUser(empid, request);
            }
        }
    }
}
