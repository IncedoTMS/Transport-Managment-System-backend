using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagementSystemBackend.Infrastructure.Data.Contexts;
using TransportManagementSystemBackend.Infrastructure.Data.Entities;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace TransportManagmentSystemBackend.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        string key = "1prt56";
        private readonly IMapper _mapper;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly TMSContext appDbContext;

        public UserRepository(IMapper mapper, TMSContext _context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            appDbContext = _context;
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
                    Password = Encryptword(request.Password),
                    Phone = request.Phone,
                    RoleId = request.RoleId,
                    AddressId = request.AddressId,
                    Department = request.Department,
                    ProjectId = request.ProjectId,
                    ProjectName = request.ProjectName,
                    Manager = request.Manager,
                    Office = request.Office,
                    AddressDetails = request.AddressDetails
                });

                int id = appDbContext.SaveChanges();

                response.Id = id;
                response.FirstName = request.FirstName;
                response.LastName = request.LastName;
                response.EmpCode = request.EmpCode;
                response.Email = request.Email;
                response.Password = Encryptword(request.Password);
                response.Phone = request.Phone;
                response.RoleId = request.RoleId;
                response.AddressId = request.AddressId;
                response.Department = request.Department;
                response.ProjectId = request.ProjectId;
                response.ProjectName = request.ProjectName;
                response.Manager = request.Manager;
                response.Office = request.Office;
                response.AddressDetails = request.AddressDetails;
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in InsertUser For User is {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserResponse> UpdateThisUser(int id, UserRequest request)
        {
            var response = new UserResponse();

            try
            {
                var user = await appDbContext.Users.FindAsync(id);

                if (user != null)
                {

                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.EmpCode = request.EmpCode;
                    user.Email = request.Email;
                    user.Phone = request.Phone;
                    user.RoleId = request.RoleId;
                    user.AddressId = request.AddressId;
                    user.Department = request.Department;
                    user.ProjectId = request.ProjectId;
                    user.ProjectName = request.ProjectName;
                    user.Manager = request.Manager;
                    user.Office = request.Office;
                    user.AddressDetails = request.AddressDetails;

                    await appDbContext.SaveChangesAsync();

                    response.Id = user.Id;
                    response.FirstName = user.FirstName;
                    response.LastName = user.LastName;
                    response.EmpCode = user.EmpCode;
                    response.Email = user.Email;
                    response.Phone = user.Phone;
                    response.RoleId = user.RoleId;
                    response.AddressId = user.AddressId;
                    response.Department = user.Department;
                    response.ProjectId = user.ProjectId;
                    response.ProjectName = user.ProjectName;
                    response.Manager = user.Manager;
                    response.Office = user.Office;
                    response.AddressDetails = user.AddressDetails;
                }
                else
                {
                    return null;
                }

                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return null;
            }
        }

        public virtual async Task<UserLoginResponse> GetUserDetails(UserLoginRequest request)
        {
            try
            {
                var id = request.UserName;
                var users = await appDbContext.Users.FirstOrDefaultAsync(x => x.Email == id && x.RoleId == request.RoleId);
                var temp = users;
                var response = new UserLoginResponse();
                
                if (users != null && Encryptword(request.Password) == users.Password)
                {
                    response.FirstName = users.FirstName;
                    response.LastName = users.LastName;
                    response.EmpCode = users.EmpCode;
                    response.UserName = users.Email;
                    response.Phone = users.Phone;
                    response.RoleId = users.RoleId;
                    response.AddressId = users.AddressId;
                    response.Department = users.Department;
                    response.ProjectId = users.ProjectId;
                    response.ProjectName = users.ProjectName;
                    response.Manager = users.Manager;
                    response.Office = users.Office;
                    response.AddressDetails = users.AddressDetails;
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in GetUserDetails For User is {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserResponse> GetUserDatabyId(int Id)
        {
            try
            {
                var users = await appDbContext.Users.FindAsync(Id);
                var response = new UserResponse();
                
                if (users != null)
                {
                    response.Id = users.Id;
                    response.FirstName = users.FirstName;
                    response.LastName = users.LastName;
                    response.EmpCode = users.EmpCode;
                    response.Email = users.Email;
                    response.Phone = users.Phone;
                    response.RoleId = users.RoleId;
                    response.AddressId = users.AddressId;
                    response.Department = users.Department;
                    response.ProjectId = users.ProjectId;
                    response.ProjectName = users.ProjectName;
                    response.Manager = users.Manager;
                    response.Office = users.Office;
                    response.AddressDetails = users.AddressDetails;
                }

                return response;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in GetAllUsers For User is {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteThisUser(int id)
        {
            try
            {
                var user = await appDbContext.Users.FindAsync(id);

                if (user != null)
                {
                    appDbContext.Users.Remove(user);

                    await appDbContext.SaveChangesAsync();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
        }

        public async Task<List<UserResponse>> GetUsersData(int? EmpCode, string Name, string Email)
        {
            try
            {
                if (EmpCode != null)
                {
                    return await appDbContext.Users.Select(x => new UserResponse
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        EmpCode = x.EmpCode,
                        Email = x.Email,
                        Phone = x.Phone,
                        RoleId = x.RoleId,
                        AddressId = x.AddressId,
                        Department = x.Department,
                        ProjectId = x.ProjectId,
                        ProjectName = x.ProjectName,
                        Manager = x.Manager,
                        Office = x.Office,
                        AddressDetails = x.AddressDetails,
                    }).Where(x => x.EmpCode.ToString().Contains(EmpCode.ToString())).ToListAsync();
                }
                else if (Name != null)
                {
                    return await appDbContext.Users.Select(x => new UserResponse
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        EmpCode = x.EmpCode,
                        Email = x.Email,
                        Phone = x.Phone,
                        RoleId = x.RoleId,
                        AddressId = x.AddressId,
                        Department = x.Department,
                        ProjectId = x.ProjectId,
                        ProjectName = x.ProjectName,
                        Manager = x.Manager,
                        Office = x.Office,
                        AddressDetails = x.AddressDetails,
                    }).Where(x => (x.FirstName + " " + x.LastName).Contains(Name)).ToListAsync();
                }
                else if (Email != null)
                {
                    return await appDbContext.Users.Select(x => new UserResponse
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        EmpCode = x.EmpCode,
                        Email = x.Email,
                        Phone = x.Phone,
                        RoleId = x.RoleId,
                        AddressId = x.AddressId,
                        Department = x.Department,
                        ProjectId = x.ProjectId,
                        ProjectName = x.ProjectName,
                        Manager = x.Manager,
                        Office = x.Office,
                        AddressDetails = x.AddressDetails,
                    }).Where(x => x.Email.Contains(Email)).ToListAsync();
                }
                else
                {
                    return await appDbContext.Users.Select(x => new UserResponse
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        EmpCode = x.EmpCode,
                        Email = x.Email,
                        Phone = x.Phone,
                        RoleId = x.RoleId,
                        AddressId = x.AddressId,
                        Department = x.Department,
                        ProjectId = x.ProjectId,
                        ProjectName = x.ProjectName,
                        Manager = x.Manager,
                        Office = x.Office,
                        AddressDetails = x.AddressDetails,
                    }).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in GetAllUsers For User is {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public string Encryptword(string Encryptval)
        {
            byte[] SrctArray;
            byte[] EnctArray = UTF8Encoding.UTF8.GetBytes(Encryptval);
            SrctArray = UTF8Encoding.UTF8.GetBytes(key);
            TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objcrpt = new MD5CryptoServiceProvider();
            SrctArray = objcrpt.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            objcrpt.Clear();
            objt.Key = SrctArray;
            objt.Mode = CipherMode.ECB;
            objt.Padding = PaddingMode.PKCS7;
            ICryptoTransform crptotrns = objt.CreateEncryptor();
            byte[] resArray = crptotrns.TransformFinalBlock(EnctArray, 0, EnctArray.Length);
            objt.Clear();
            return Convert.ToBase64String(resArray, 0, resArray.Length);
        }
    }
}
