using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;
using TransportManagmentSystemBackend.Infrastructure.Data.Entities;

namespace TransportManagmentSystemBackend.Core.Interfaces.Repositories
{
    public interface IJWTRepo
    {
        Tokens Authenticate(UserLoginRequest users);
    }
}
