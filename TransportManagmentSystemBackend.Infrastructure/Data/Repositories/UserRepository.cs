using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementSystemBackend.Infrastructure.Data.Contexts;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;

namespace TransportManagmentSystemBackend.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly TMSContext appDbContext;

        public UserRepository(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserResponse> InsertUser(UserRequest request)
        {
            try
            {
                var response = new UserResponse();

                appDbContext.Users.Add(new TransportManagementSystemBackend.Infrastructure.Data.Entities.User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmpCode = request.EmpCode,
                    Email = request.Email,
                    Password = request.Password,
                    Phone = request.Phone,
                    RoleId = request.RoleId,
                    AddressId = request.AddressId,
                });

                int id = appDbContext.SaveChanges();

                response.Id = id;
                response.FirstName = request.FirstName;
                response.LastName = request.LastName;
                response.EmpCode = request.EmpCode;
                response.Email = request.Email;
                response.Password = request.Password;
                response.Phone = request.Phone;
                response.RoleId = request.RoleId;
                response.AddressId = request.AddressId;
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in InsertUser For User is {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
