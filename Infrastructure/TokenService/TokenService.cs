using Application.Common.Interfaces;
using FluentValidation.AspNetCore;
using Infrastructure.InfrastructureService.JWTToken.CreateToken;
using Infrastructure.InfrastructureService.JWTToken;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Application.Common.Interfaces;
using Northwind.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.TokenService
{
    public static class TokenService
    {
        public static void AddAuthentication(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {

                    ValidateIssuer = false,//it mean the server that issue the token
                    ValidateAudience = false,// it means the client that send the request
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                    /*                    ValidAudience = Configuration["JWT:ValidAudience"],
                                        ValidIssuer = Configuration["JWT:ValidIssuer"],*/
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettingModel:Secret"]))
                    /**/
                };
            });

            services.AddScoped<IJWTToken, JWTToken>();
            services.AddScoped<ICreateToken, CreateToken>();
        }
    }
}
