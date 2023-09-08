using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.common.Interfaces.Files
{
    public interface IPhotoFilesService
    {
        Dictionary<string, IFormFile> GetValidFilesNewNames(IFormFileCollection files);

        Task<RespDto<bool>> StoreValidPhotoFilesOnServer(Dictionary<string, IFormFile> files);

        RespDto<bool> RemovePhotoFilesFromServer(List<string> fileNames);
    }
}
