using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransportManagmentSystemBackend;

namespace TransportManagmentSystemBackend.Core.Data
{
    public class UserRequestContext : DbContext
    {
        public UserRequestContext(DbContextOptions<UserRequestContext> options)
            : base(options)
        {
        }

        public DbSet<TransportManagmentSystemBackend.Core.Domain.Models.UserRequest> UserRequest { get; set; }
    }
}

