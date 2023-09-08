using AcademyProject.Services;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Database.UnitOfWork;
using Infrastructure.Infrastructure.Identity;
using Infrastructure.UserIdentity.UserManagerApp;
using Infrastructure.UserIdentity.UserRoleApp;
using Infrastructure.UserIdentity.UserSignInManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UserIdentity
{
    public static class IdentityService
    {
        public static void AddIdentityService(IServiceCollection services)
        {
            // Add identity to application service for injection all classes related to it
            // Ex userManager without need to add it to dependency injection
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()//.AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();


            // add specific configuration to identity
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 10;

                // Required Confirmation
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;

                // Username Configuration 
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                options.User.RequireUniqueEmail = false;


            });

            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<IUserSignInManagerService, UserSignInManagerService>();
            services.AddScoped<IUserRoleManagerService, UserRoleManagerService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

        }
    }
}
