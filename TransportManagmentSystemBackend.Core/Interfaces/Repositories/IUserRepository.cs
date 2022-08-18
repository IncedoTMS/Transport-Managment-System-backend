﻿using Microsoft.AspNetCore.Mvc;
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
        Task<List<UserResponse>> GetAllUsers();
        Task<UserResponse> DeleteThisUser(int id);
        Task<UserLoginResponse> GetUserDetails(UserLoginRequest request);
    }
}
