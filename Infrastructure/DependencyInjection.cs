using Infrastructure.Database;
using Infrastructure.UserIdentity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.TokenService;
using Infrastructure.configuration;
using Infrastructure.Logger;
using Microsoft.AspNetCore.Builder;
using System;
using Infrastructure.Files;
using AcademyProject.Services;
using Application.Common.Interfaces;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this WebApplicationBuilder builder)
        {
            ConfigurationManager configuration = builder.Configuration;
            IWebHostEnvironment environment = builder.Environment;
            IServiceCollection service = builder.Services;
            
            var DbConnectionString = configuration.GetConnectionString("Default");
            var userConnectionStr = configuration.GetConnectionString("Users");

            DbService.AddDbService(service,DbConnectionString!);

            UserDbService.AddUserDbService(service,userConnectionStr!);

            IdentityService.AddIdentityService(service);

            TokenService.TokenService.AddAuthentication(service, configuration);

            ConfigurationService.AddConfigurationService(service, configuration);

            Loggerservice.AddLoggingService(builder);

            HandleFilesService.AddFileServices(service);

        }

    }
}