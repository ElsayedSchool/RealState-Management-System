using Application.common.Interfaces.Files;
using Infrastructure.Files.FileIOService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Files
{
    public static class HandleFilesService
    {
        public static void AddFileServices(this IServiceCollection services)
        {
            services.AddScoped<IExcelFileService, ExcelFilesService>();
            services.AddScoped<IPhotoFilesService, PhotoFilesService>();
        }
    }
}
