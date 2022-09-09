using AutoMapper;
//using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TransportManagementSystemBackend.Infrastructure.Data.Contexts;
using TransportManagmentSystemBackend.Core.Domain.Enum;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;
using TransportManagmentSystemBackend.Infrastructure.Data.Entities;

namespace TransportManagmentSystemBackend.Infrastructure.Data.Repositories
{
    public class JWTRepo : IJWTRepo
    {
		string key = "1prt56";

		private readonly IConfiguration iconfiguration;
		private readonly IMapper _mapper;
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private readonly TMSContext appDbContext;

		public JWTRepo(IMapper mapper, TMSContext _context, IConfiguration iconfiguration)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			appDbContext = _context;
			this.iconfiguration = iconfiguration;
		}
		public Tokens Authenticate(UserLoginRequest users)
		{
			var usersDetail = appDbContext.Users.FirstOrDefault(x => x.Email == users.UserName && x.Password == Encryptword(users.Password));
			if (usersDetail == null)
			{
				return null;
			}
			int value = usersDetail.RoleId;
			string RoleOfLogin = Enum.GetName(typeof(Role), value);

			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[] {new Claim(ClaimTypes.Name, users.UserName), new Claim(ClaimTypes.Role, RoleOfLogin)}),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { Token = tokenHandler.WriteToken(token) };

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
