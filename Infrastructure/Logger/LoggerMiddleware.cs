using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logger
{
    public static class LoggerMiddleware
    {
        public static void SetLoggerMiddleware(this WebApplication app)
        {
            app.UseSerilogRequestLogging(configure =>
            {
                configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({Id}) responded {StatusCode} in {Elapsed:0.0000}ms";
                configure.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Warning;
                configure.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    var currentUser = httpContext.User.FindFirstValue("Id");
                    diagnosticContext.Set("Id", currentUser);
                };
            });
        }
    }
}
