using AcademyProject.Services;
using Application;
using Application.Authentication;
using Application.Common.Interfaces;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Database;
using Newtonsoft.Json;

namespace AcademyProject
{
    public static class ConfigureAppService
    {
        public static WebApplicationBuilder SetAppServices(this WebApplicationBuilder builder)
        {
            var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()!;
            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(
              options => {
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
              });
            //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SignUpCommandValidator>());


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            


            // Inject all Dependencies in All Three Layers
            builder.Services.AddApplication();
            builder.AddInfrastructure();
            builder.Services.ConfigureApiBehavior();

            // Add Current User injection
            /*builder.Services.AddHealthChecks()
                            .AddDbContextCheck<ApplicationDbContext>();*/

            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddCors(c => c.AddPolicy("mypolicy", builder =>
            {
                builder
                .WithOrigins(allowedOrigins)
                .WithHeaders("content-type", "Authorization")
                .WithMethods("GET", "POST", "DELETE", "PUT");
            }));


            builder.Services.AddHttpContextAccessor();
           
            return builder;
        }
    }
}
