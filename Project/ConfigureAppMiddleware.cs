

using AcademyProject.MiddleWares;
using Infrastructure.Logger;
using Microsoft.Extensions.FileProviders;

namespace AcademyProject
{
    public static class ConfigureAppMiddleware
    {
        public static WebApplication SetAppMiddleWare(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCustomExceptionHandler();


            app.UseHttpsRedirection();

            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(app.Environment.WebRootPath, "Images")),
                RequestPath = "/Photos",
                ServeUnknownFileTypes = false,
                DefaultContentType = "image/png",
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "public, max-age=86400");
                }
            });

            app.UseCors("mypolicy");

            app.UseAuthentication();

            //app.UseIdentityServer();

            app.UseAuthorization();

            app.MapControllers();


            app.SetLoggerMiddleware();

            app.Run();
            return app;
        }
    }
}
