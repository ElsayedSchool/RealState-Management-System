using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Services
{
    public static class ApiBehaviorService
    {

        public static void ConfigureApiBehavior(this IServiceCollection services)
        {
            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
