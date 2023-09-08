using Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.configuration
{
    // it will be injected using IOption<AppSettingModel> in Constructor where it is need
    public class ConfigurationService
    {
        public static void AddConfigurationService(IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<AppSettingModel>(configuration.GetSection("AppSettingModel"));
            services.Configure<EmailSetting>(configuration.GetSection("SMTP"));
        }
    }
}
