using Application.common.Interfaces.Files;
using Application.Common.Models;
using Domain.Enums;
using Infrastructure.Files.FileIOService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Infrastructure.Files
{
    public class PhotoFilesService : IPhotoFilesService
    {
        private readonly ILogger<ExcelFilesService> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly string _imageFolderPath;

        public PhotoFilesService(ILogger<ExcelFilesService> logger, IWebHostEnvironment webHost)
        {
            _logger = logger;
            _webHost = webHost;
            _imageFolderPath = Path.Combine(_webHost.WebRootPath, "Images");
        }

       
        public Dictionary<string, IFormFile> GetValidFilesNewNames(IFormFileCollection files)
        {
            var imagesPaths = new Dictionary<string, IFormFile>();
            foreach (var file in files)
            {
                if (IsFileValid(file))
                {
                    var fileName = Guid.NewGuid().ToString() + file.FileName;
                        
                    imagesPaths[fileName] = file;
                }
            }

            return imagesPaths;
        }

        public async Task<RespDto<bool>> StoreValidPhotoFilesOnServer(Dictionary<string, IFormFile> files)
        {
            try
            {
                foreach(var file in files)
                {
                    var filePath = Path.Combine(_imageFolderPath, file.Key);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.Value.CopyToAsync(stream);
                    }
                }
                return new RespDto<bool>() {  Data = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطا اثناء حفظ الصور");
                return new RespDto<bool>() { Data = false, Error = true, Message = "حدث خطا اثناء حفظ الصور" };
            }
            
        }

        public RespDto<bool> RemovePhotoFilesFromServer(List<string> fileNames)
        {
            try
            {
                string[] files = Directory.GetFiles(_imageFolderPath);
                foreach (var file in fileNames)
                {
                    var filePath = Path.Combine(_imageFolderPath, file);
                    if (Path.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                return new RespDto<bool>() { Data = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطا اثناء ازاله الصور");
                return new RespDto<bool>() { Data = false, Error = true, Message = "حدث خطا اثناء ازاله الصور" };
            }
        }

        private bool IsFileValid(IFormFile file)
        {
            var supportedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            if (file == null || file.Length == 0) return false;

            if (!file.ContentType.StartsWith("image/")) return false;

            if (!supportedExtensions.Contains(Path.GetExtension(file.FileName))) return false;

            return true;
        }
    }
}
