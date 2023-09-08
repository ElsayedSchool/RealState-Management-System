using Application.Common.Interfaces;
using Infrastructure.Database.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public static class DbService
    {
        public static void AddDbService(IServiceCollection service, string appConStr)
        {
            //var connection = ConnectionSetting.CreateConnectionString();
            //var conStr = "server=SQL5110.site4now.net;Database = db_a96acd_dboffers31;User Id=db_a96acd_dboffers31_admin;Password=EngElsayed#31@DbOff";
            service.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(appConStr));
            service.AddScoped<IApplicationRepo, ApplicationRepo>();
        }

    }
}
