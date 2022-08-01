using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;

namespace TransportManagmentSystemBackend.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public UserRepository(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<UserResponse> InsertUser(UserRequest request)
        {
            try
            {
                return Task.FromResult(new UserResponse()
                {
                    Id = 1,
                    FirstName = "FirstTest",
                    LastName = "LastName",
                    Email ="test@test.com",
                    Phone = "1234567890"
                });
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in InsertUser For User is {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
