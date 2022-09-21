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
        private readonly IEmailSender _emailSender;

        public UserRepository(IMapper mapper, TMSContext _context, IEmailSender emailSender)
        {
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.appDbContext = _context;
            this._emailSender = emailSender;
        }

        public async Task<UserResponse> InsertUser(UserRequest request)
        {
            try
            {
                var manager = await appDbContext.Managers.FindAsync(request.ManagerId);
                if (manager == null && request.ManagerId != null)
                {
                    var newpass = Password.Generate(15, 2);
                    appDbContext.Managers.Add(new Manager
                    {
                        Id = (int)request.ManagerId,
                        ManagerEmail = request.ManagerEmail,
                        ManagerName = request.ManagerName,
                        Password = Encryptword(newpass)
                    });
                    string emailmessage = "Hi,\n\nA new manager has been created.\nManager Details\nManager Id: "
                        +request.ManagerId
                        +"\nManager Email: "
                        +request.ManagerEmail
                        +"\nManager Password: "
                        +newpass
                        +"\n\nThanks,\nTMS Team";
                    var message = new Message(new string[] { request.ManagerEmail }, "Created new Manager", emailmessage);
                    _emailSender.SendEmail(message);
                }
                var response = new UserResponse();
                appDbContext.Users.Add(new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Id = request.EmpCode,
                    Email = request.Email,
                    Password = Encryptword(request.Password),
                    Phone = request.Phone,
                    RoleId = request.RoleId,
                    AddressId = request.AddressId,
                    Department = request.Department,
                    ProjectId = request.ProjectId,
                    ProjectName = request.ProjectName,
                    ManagerId = request.ManagerId,
                    Office = request.Office,
                    AddressDetails = request.AddressDetails
                });

                int id = appDbContext.SaveChanges();

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
                response.ManagerId = request.ManagerId;
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
                var manager = await appDbContext.Managers.FindAsync(request.ManagerId);
                if (manager == null && request.ManagerId != null)
                {
                    var newpass = Password.Generate(15, 2);
                    appDbContext.Managers.Add(new Manager
                    {
                        Id = (int)request.ManagerId,
                        ManagerEmail = request.ManagerEmail,
                        ManagerName = request.ManagerName,
                        Password = Encryptword(newpass)
                    });
                    string emailmessage = "Hi,\n\nA new manager has been created.\nManager Details\nManager Id: "
                        + request.ManagerId
                        + "\nManager Email: "
                        + request.ManagerEmail
                        + "\nManager Password: "
                        + newpass
                        + "\n\nThanks,\nTMS Team";
                    var message = new Message(new string[] { request.ManagerEmail }, "Created new Manager", emailmessage);
                    _emailSender.SendEmail(message);
                }
                if (user != null)
                {
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.Id = request.EmpCode;
                    user.Email = request.Email;
                    user.Phone = request.Phone;
                    user.RoleId = request.RoleId;
                    user.AddressId = request.AddressId;
                    user.Department = request.Department;
                    user.ProjectId = request.ProjectId;
                    user.ProjectName = request.ProjectName;
                    user.ManagerId = request.ManagerId;
                    user.Office = request.Office;
                    user.AddressDetails = request.AddressDetails;

                    await appDbContext.SaveChangesAsync();

                    response.EmpCode = user.Id;
                    response.FirstName = user.FirstName;
                    response.LastName = user.LastName;
                    response.Email = user.Email;
                    response.Phone = user.Phone;
                    response.RoleId = user.RoleId;
                    response.AddressId = user.AddressId;
                    response.Department = user.Department;
                    response.ProjectId = user.ProjectId;
                    response.ProjectName = user.ProjectName;
                    response.ManagerId = user.ManagerId;
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
                if (request.RoleId != 3)
                {
                    var users = await appDbContext.Users.FirstOrDefaultAsync(x => x.Email == id && x.RoleId == request.RoleId);
                    var temp = users;
                    var response = new UserLoginResponse();
                    if (users != null && Encryptword(request.Password) == users.Password)
                    {
                        response.FirstName = users.FirstName;
                        response.LastName = users.LastName;
                        response.EmpCode = users.Id;
                        response.UserName = users.Email;
                        response.Phone = users.Phone;
                        response.RoleId = users.RoleId;
                        response.AddressId = users.AddressId;
                        response.Department = users.Department;
                        response.ProjectId = users.ProjectId;
                        response.ProjectName = users.ProjectName;
                        response.ManagerId = users.ManagerId;
                        response.Office = users.Office;
                        response.AddressDetails = users.AddressDetails;
                    }

                    return response;
                }
                else
                {
                    var managers = await appDbContext.Managers.FirstOrDefaultAsync(x => x.ManagerEmail == id && request.RoleId == 3);
                    var response = new UserLoginResponse();
                    if (managers != null && Encryptword(request.Password) == managers.Password)
                    {
                        response.FirstName = managers.ManagerName;
                        response.EmpCode = managers.Id;
                        response.UserName = managers.ManagerEmail;
                    }
                    return response;
                }
                
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
                    response.EmpCode = users.Id;
                    response.FirstName = users.FirstName;
                    response.LastName = users.LastName;
                    response.Email = users.Email;
                    response.Phone = users.Phone;
                    response.RoleId = users.RoleId;
                    response.AddressId = users.AddressId;
                    response.Department = users.Department;
                    response.ProjectId = users.ProjectId;
                    response.ProjectName = users.ProjectName;
                    response.ManagerId = users.ManagerId;
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
        public async Task<UserResponse> DeleteThisUser(int id)
        {
            var response = new UserResponse();
            try
            {
                var user = await appDbContext.Users.FindAsync(id);

                if (user != null)
                {
                    response.EmpCode = user.Id;
                    response.FirstName = user.FirstName;
                    response.LastName = user.LastName;
                    response.Email = user.Email;
                    response.Phone = user.Phone;
                    response.RoleId = user.RoleId;
                    response.AddressId = user.AddressId;
                    response.Department = user.Department;
                    response.ProjectId = user.ProjectId;
                    response.ProjectName = user.ProjectName;
                    response.ManagerId = user.ManagerId;
                    response.Office = user.Office;
                    response.AddressDetails = user.AddressDetails;

                    appDbContext.Users.Remove(user);

                    await appDbContext.SaveChangesAsync();
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

        public async Task<List<UserResponse>> GetUsersData(int? EmpCode, string Name, string Email, int? ManagerId)
        {
            try
            {
                if (EmpCode != null)
                {
                    return await appDbContext.Users.Select(x => new UserResponse
                    {
                        EmpCode = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        Phone = x.Phone,
                        RoleId = x.RoleId,
                        AddressId = x.AddressId,
                        Department = x.Department,
                        ProjectId = x.ProjectId,
                        ProjectName = x.ProjectName,
                        ManagerId = x.ManagerId,
                        Office = x.Office,
                        AddressDetails = x.AddressDetails,
                    }).Where(x => x.EmpCode.ToString().Contains(EmpCode.ToString())).ToListAsync();
                }
                else if (Name != null)
                {
                    return await appDbContext.Users.Select(x => new UserResponse
                    {
                        EmpCode = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        Phone = x.Phone,
                        RoleId = x.RoleId,
                        AddressId = x.AddressId,
                        Department = x.Department,
                        ProjectId = x.ProjectId,
                        ProjectName = x.ProjectName,
                        ManagerId = x.ManagerId,
                        Office = x.Office,
                        AddressDetails = x.AddressDetails,
                    }).Where(x => (x.FirstName + " " + x.LastName).Contains(Name)).ToListAsync();
                }
                else if (Email != null)
                {
                    return await appDbContext.Users.Select(x => new UserResponse
                    {
                        EmpCode = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        Phone = x.Phone,
                        RoleId = x.RoleId,
                        AddressId = x.AddressId,
                        Department = x.Department,
                        ProjectId = x.ProjectId,
                        ProjectName = x.ProjectName,
                        ManagerId = x.ManagerId,
                        Office = x.Office,
                        AddressDetails = x.AddressDetails,
                    }).Where(x => x.Email.Contains(Email)).ToListAsync();
                }
                else if (ManagerId != null)
                {
                    return await appDbContext.Users.Select(x => new UserResponse
                    {
                        EmpCode = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        Phone = x.Phone,
                        RoleId = x.RoleId,
                        AddressId = x.AddressId,
                        Department = x.Department,
                        ProjectId = x.ProjectId,
                        ProjectName = x.ProjectName,
                        ManagerId = x.ManagerId,
                        Office = x.Office,
                        AddressDetails = x.AddressDetails,
                    }).Where(x => x.ManagerId.ToString().Contains(ManagerId.ToString())).ToListAsync();
                }
                else
                {
                    return await appDbContext.Users.Select(x => new UserResponse
                    {
                        EmpCode = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        Phone = x.Phone,
                        RoleId = x.RoleId,
                        AddressId = x.AddressId,
                        Department = x.Department,
                        ProjectId = x.ProjectId,
                        ProjectName = x.ProjectName,
                        ManagerId = x.ManagerId,
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

        public async Task<List<ManagerResponse>> GetManagersData()
        {
            try
            {
                return await appDbContext.Managers.Select(x => new ManagerResponse
                {
                    ManagerEmail = x.ManagerEmail,
                    ManagerId = x.Id,
                    ManagerName = x.ManagerName,
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occurs in GetAllUsers For User is {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<ChangePasswordResponse> UpdateThisPassword(int Id, string Email, int RoleId, string Password)
        {
            try
            {
                var response = new ChangePasswordResponse();
                if (RoleId == 3)
                {
                    var managerdata = await appDbContext.Managers.FindAsync(Id);
                    if (managerdata != null && managerdata.ManagerEmail == Email)
                    {
                        managerdata.Password = Encryptword(Password);

                        await appDbContext.SaveChangesAsync();

                        response.Id = managerdata.Id;
                        response.Email = managerdata.ManagerEmail;
                        response.Password = managerdata.Password;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    var userdata = await appDbContext.Users.FindAsync(Id);
                    if (userdata != null && userdata.Email == Email)
                    {
                        userdata.Password = Encryptword(Password);

                        await appDbContext.SaveChangesAsync();

                        response.Id = userdata.Id;
                        response.Email = userdata.Email;
                        response.Password = userdata.Password;
                    }
                    else
                    {
                        return null;
                    }
                }
                
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return null;
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
    public static class Password
    {
        private static readonly char[] Punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();

        public static string Generate(int length, int numberOfNonAlphanumericCharacters)
        {
            if (length < 1 || length > 128)
            {
                throw new ArgumentException(nameof(length));
            }

            if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
            {
                throw new ArgumentException(nameof(numberOfNonAlphanumericCharacters));
            }

            using (var rng = RandomNumberGenerator.Create())
            {
                var byteBuffer = new byte[length];

                rng.GetBytes(byteBuffer);

                var count = 0;
                var characterBuffer = new char[length];

                for (var iter = 0; iter < length; iter++)
                {
                    var i = byteBuffer[iter] % 87;

                    if (i < 10)
                    {
                        characterBuffer[iter] = (char)('0' + i);
                    }
                    else if (i < 36)
                    {
                        characterBuffer[iter] = (char)('A' + i - 10);
                    }
                    else if (i < 62)
                    {
                        characterBuffer[iter] = (char)('a' + i - 36);
                    }
                    else
                    {
                        characterBuffer[iter] = Punctuations[i - 62];
                        count++;
                    }
                }

                if (count >= numberOfNonAlphanumericCharacters)
                {
                    return new string(characterBuffer);
                }

                int j;
                var rand = new Random();

                for (j = 0; j < numberOfNonAlphanumericCharacters - count; j++)
                {
                    int k;
                    do
                    {
                        k = rand.Next(0, length);
                    }
                    while (!char.IsLetterOrDigit(characterBuffer[k]));

                    characterBuffer[k] = Punctuations[rand.Next(0, Punctuations.Length)];
                }

                return new string(characterBuffer);
            }
        }
    }
}
