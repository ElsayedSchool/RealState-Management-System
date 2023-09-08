using Infrastructure.Database;
using Infrastructure.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UserIdentity
{
    public static class UserDbService
    {
        public static void AddUserDbService(IServiceCollection service, string UserConStr)
        {
            /*service.AddDbContext<UserDbContext>(options => options.UseSqlServer(UserConStr));*/
        }
    }
}
