using AcademyProject.Controllers;
using Application.CommentApp.Command.UpsertComment;
using Application.common.Interfaces.Files;
using Application.FavoriteApp.Commands.IsFavorite;
using Application.FavoriteApp.Commands.UpsertFavorite;
using Application.FavoriteApp.Queries;
using Application.FileApp.Commands.AddUploadedOffers;
using Application.FileApp.Commands.ExportDefaultTemplate;
using Application.FileApp.Commands.ExportSelectedOffers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace realState.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : BaseController
    {
        private readonly IExcelFileService _excelFile;

        public FilesController(IExcelFileService excelFile)
        {
            _excelFile = excelFile;
        }

        [HttpGet]
        public async Task<ActionResult> GetDefaultTemplate()
        {
            var filePath = await Mediator.Send(new ExportDefaultTemplateCommand());
            if(filePath == null || filePath.Data == null || filePath.Error == true) return Ok(filePath);
            using var stream = new FileStream(filePath.Data, FileMode.Open, FileAccess.Read);
            var memStream = new MemoryStream();
            var fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            await stream.CopyToAsync(memStream);
            memStream.Position = 0;
            return File(memStream, fileType, "Offers.xlsx");
        }

        // POST api/<FilesController>
        [HttpPost("addoffers")]
        public async Task<ActionResult> AddOffers([FromForm] AddUploadedOffersCommand command)
        {
            var filePath = await Mediator.Send(command);
            if (filePath == null || filePath.Data == null || filePath.Error == true || filePath.Data == "") return Ok(filePath);
            using var stream = new FileStream(filePath.Data, FileMode.Open, FileAccess.Read);
            var memStream = new MemoryStream();
            var fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            await stream.CopyToAsync(memStream);
            memStream.Position = 0;
            return File(memStream, fileType, "Offers.xlsx");
        }

        [HttpPost("exportselectedoffers")]
        public async Task<ActionResult> ExportSelectedOffers([FromBody] ExportSelectedOffersCommand command)
        {
            var filePath = await Mediator.Send(command);
            if (filePath == null || filePath.Data == null || filePath.Error == true) return Ok(filePath);
            using var stream = new FileStream(filePath.Data, FileMode.Open, FileAccess.Read);
            var memStream = new MemoryStream();
            var fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            await stream.CopyToAsync(memStream);
            memStream.Position = 0;
            return File(memStream, fileType, "Offers.xlsx");
        }

    }
}
