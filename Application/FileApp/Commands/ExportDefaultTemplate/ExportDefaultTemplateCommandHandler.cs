using Application.common.Interfaces.Files;
using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileApp.Commands.ExportDefaultTemplate
{
    public class ExportDefaultTemplateCommandHandler : IRequestHandler<ExportDefaultTemplateCommand, string>
    {
        private readonly IExcelFileService _fileService;
        private readonly IApplicationRepo _repo;

        public ExportDefaultTemplateCommandHandler(IExcelFileService fileService, IApplicationRepo repo)
        {
            _fileService = fileService;
            _repo = repo;
        }

        public async Task<RespDto<string>> Handle(ExportDefaultTemplateCommand request, CancellationToken cancellationToken)
        {
            var lookups = await _repo.CategoryRepo.GetAllAsync();
            if(lookups == null || lookups.Data?.Count() == 0 || lookups.Data == null)
            {
                return new RespDto<string>() { Data = "D:\\1-data\\1-Projects\\5-realState\\1-realState_back\\1-realState_back\\wwwroot\\ExcelFiles\\ImportTemplate.xlsx" };
            }

            return new RespDto<string>() { Data = _fileService.AddLookupsToDefaultTemplate(lookups.Data) };
        }
    }
}
