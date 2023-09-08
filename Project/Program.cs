using AcademyProject;
using Application;
using Application.Authentication;
using Application.Common.Interfaces;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Database;
using Serilog;


try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.SetAppServices();

    var app = builder.Build();
    var logger = app.Services.CreateScope().ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        logger.LogWarning("Updated Application is running");
        await SeedData.Seed(app);
    }
    catch (Exception ex)
    {
        logger.LogError(ex.Message);
    }
    app.SetAppMiddleWare();

}
catch (Exception ex)
{

	throw;
}
finally
{

}

return 0;

