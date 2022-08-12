﻿using AutoMapper;
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
using System.Security.Cryptography;namespace TransportManagmentSystemBackend.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository 
    { string key = "1prt56"; 
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
                }); 
                int id = appDbContext.SaveChanges(); 
                response.Id = id; response.FirstName = request.FirstName; 
                response.LastName = request.LastName; 
                response.EmpCode = request.EmpCode; 
                response.Email = request.Email; 
                response.Password = Encryptword(request.Password);
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
        public async Task<UserResponse> EditThisUser(int id, UserRequest request) 
        {
            var response = new UserResponse(); 
            try { 
                var user = await appDbContext.Users.FindAsync(id); 
                if (user != null)
                { 
                    response.FirstName = request.FirstName;
                    response.LastName = request.LastName; 
                    response.EmpCode = request.EmpCode; 
                    response.Email = request.Email; 
                    response.Password = Encryptword(request.Password);
                    response.Phone = request.Phone;
                    response.RoleId = request.RoleId; 
                    response.AddressId = request.AddressId; 
                    user.FirstName = response.FirstName; 
                    user.LastName = response.LastName;
                    user.EmpCode = response.EmpCode; 
                    user.Email = response.Email; 
                    user.Password = response.Password; 
                    user.Phone = response.Phone; 
                    user.RoleId = response.RoleId; 
                    user.AddressId = response.AddressId;
                    
                    await appDbContext.SaveChangesAsync();
                } else { return null; 
                } 
                return response;
            } catch (DbUpdateConcurrencyException ex) 
            { return null;
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
            objt.Clear(); return Convert.ToBase64String(resArray, 0, resArray.Length); 
        }
    }
}

