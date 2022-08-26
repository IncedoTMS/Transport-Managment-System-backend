using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Infrastructure.Data.Entities;

namespace TransportManagmentSystemBackend.Core.Services
{
    public interface IJWTService
    {
        Tokens Authentication(UserLoginRequest usersdata);
    }
}
