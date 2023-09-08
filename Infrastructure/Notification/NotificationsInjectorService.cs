using Application.Common.Interfaces;
using Infrastructure.Database.UnitOfWork;
using Infrastructure.Infrastructure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Application.Common.Interfaces;
using Northwind.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Notification
{
    public class NotificationsInjectorService
    {
        public static void AddNotificationService(IServiceCollection services)
        {
            /*services.AddTransient<INotificationService, NotificationService>();*/
            
        }
    }
}
