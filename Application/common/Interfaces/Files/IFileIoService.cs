using Application.Common.Models;
using Application.FileApp.Commands.AddUploadedOffers;
using Application.OfferApp.Queries.GetOfferDetail;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.common.Interfaces.Files
{
    public interface IExcelFileService
    {
        string AddLookupsToDefaultTemplate(List<Category> lookups);
        Task<RespDto<List<UploadedOffersVm>>> GetOffersDataFromFile(IFormFile offersFile);
        string ExportSelectedOffersExcelFile(List<OfferDetailVm> selectedOffers);
        string ExportinvalidOffersExcelFile(List<UploadedOffersVm> selectedOffers);
    }
}
