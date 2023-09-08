using Application.common.Interfaces.Files;
using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileApp.Commands.ExportSelectedOffers
{
    public class ExportSelectedOffersCommandHandler : IRequestHandler<ExportSelectedOffersCommand, string>
    {
        private readonly IApplicationRepo _repo;
        private readonly IExcelFileService _fileService;

        public ExportSelectedOffersCommandHandler(IApplicationRepo repo, IExcelFileService fileService)
        {
            _repo = repo;
            _fileService = fileService;
        }

        public async Task<RespDto<string>> Handle(ExportSelectedOffersCommand request, CancellationToken cancellationToken)
        {
            var offers = await _repo.OfferRepo.GetSelectedOffersDetails(request.selectedOffers);
            if (offers == null || offers.Error == true || offers.Data == null || offers.Data.Count() == 0) return new RespDto<string> { Error = true, Message = "حدث خطا اثناء استرجاع بيانات العروض برجاء المحاوله مره اخرئ" };
            return new RespDto<string>() { Data = _fileService.ExportSelectedOffersExcelFile(offers.Data) };
        }
    }
}
