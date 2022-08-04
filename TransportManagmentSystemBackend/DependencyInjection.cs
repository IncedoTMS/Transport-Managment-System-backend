using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransportManagmentSystemBackend.Core.AppSettings;
using TransportManagmentSystemBackend.Core.Interfaces.Repositories;
using TransportManagmentSystemBackend.Core.Services;
using TransportManagmentSystemBackend.Infrastructure.Data.Repositories;

namespace TransportManagmentSystemBackend.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var appSettings = new AppSetting();
            configuration.Bind("AppSettings", appSettings);

            string tmsDbConnectionString, tmsRoDbConnectionString = string.Empty;

            var dbenv = appSettings?.DatabaseEnvironment.ToString();

            if (!string.IsNullOrEmpty(dbenv))
            {
                dbenv = $"{dbenv}-";
            }

            tmsDbConnectionString = configuration.GetConnectionString("TransportManagmentSystem");

            //// TransportManagmentSystem database

            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ICabService, CabService>();
            services.AddSingleton<ICabRepository, CabRepository>();

            services.AddSingleton(appSettings);

            return services;
        }
    }
}
