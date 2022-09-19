using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;
using TransportManagmentSystemBackend.Infrastructure.Data.Entities;

namespace TransportManagmentSystemBackend.Core.Services
{
    public class JWTService : IJWTService
    {
        private readonly IJWTRepo _repo;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public JWTService(IJWTRepo repo)
        {
            this._repo = repo;
        }
       
        public Tokens Authentication(UserLoginRequest usersdata)
        {
            if (usersdata == null)
            {
                Logger.Error("UserService.GetUserLogin is called and getting null exception for add user");
                throw new ArgumentException(nameof(Authentication));
            }
            else
            {
                return _repo.Authenticate(usersdata);
            }
        }
    }
}
