using Application.common.Interfaces.Files;
using Application.Common.Models;
using Application.FileApp.Commands.AddUploadedOffers;
using Application.OfferApp.Queries.GetOfferDetail;
using ClosedXML.Excel;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Files.FileIOService
{
    public class ExcelFilesService : IExcelFileService
    {
        private readonly ILogger<ExcelFilesService> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly string _excelFilesPath;

        public ExcelFilesService(ILogger<ExcelFilesService> logger, IWebHostEnvironment webHost)
        {
            _logger = logger;
            _webHost = webHost;
            _excelFilesPath = Path.Combine(_webHost.WebRootPath, "ExcelFiles");
        }

        public string AddLookupsToDefaultTemplate(List<Category> lookups)
        {
            var filePath = Path.Combine(_excelFilesPath, "ImportTemplate.xlsx");
            using var file = new XLWorkbook(filePath);

            var lookupsSheet = file.Worksheets.LastOrDefault();


            for (int i = 1; i < 8; i++)
            {
                var lookupsList = lookups.Where(p => Convert.ToInt32(p.category) == i-1).ToList();
                for (int j = 1; j <= lookupsList.Count(); j++)
                {
                    lookupsSheet.Cell(j+1,i).Value = lookupsList[j - 1].Name;
                }
            }
            var newFilePath = Path.Combine(_excelFilesPath, "ImportTemplate1.xlsx");
            file.SaveAs(newFilePath);

            return newFilePath;
        }

        public string ExportinvalidOffersExcelFile(List<UploadedOffersVm> selectedOffers)
        {
            var filePath = Path.Combine(_excelFilesPath, "ImportTemplate1.xlsx");

            var newFilePath = Path.Combine(_excelFilesPath, "ImportTemplate2.xlsx");

            using var file = new XLWorkbook(filePath);

            var dataSheet = file.Worksheets.FirstOrDefault();

            for (int i = 1; i <= selectedOffers.Count(); i++)
            {
                dataSheet.Cell(i + 1, 1).Value = selectedOffers[i - 1].DepartmentName;
                dataSheet.Cell(i + 1, 2).Value = selectedOffers[i - 1].OwnerName;
                dataSheet.Cell(i + 1, 3).Value = selectedOffers[i - 1].Phone1;
                dataSheet.Cell(i + 1, 4).Value = selectedOffers[i - 1].Phone2;
                dataSheet.Cell(i + 1, 5).Value = selectedOffers[i - 1].PurposeName;
                dataSheet.Cell(i + 1, 6).Value = selectedOffers[i - 1].TypeName;
                dataSheet.Cell(i + 1, 7).Value = selectedOffers[i - 1].AreaName;
                dataSheet.Cell(i + 1, 8).Value = selectedOffers[i - 1].LocationName;
                dataSheet.Cell(i + 1, 9).Value = selectedOffers[i - 1].SectionName;
                dataSheet.Cell(i + 1, 10).Value = selectedOffers[i - 1].DistributionName;
                dataSheet.Cell(i + 1, 11).Value = selectedOffers[i - 1].Piece;
                dataSheet.Cell(i + 1, 12).Value = selectedOffers[i - 1].Kasema;
                dataSheet.Cell(i + 1, 13).Value = selectedOffers[i - 1].Street;
                dataSheet.Cell(i + 1, 14).Value = selectedOffers[i - 1].House;
                dataSheet.Cell(i + 1, 15).Value = selectedOffers[i - 1].Price;
                dataSheet.Cell(i + 1, 16).Value = selectedOffers[i - 1].Details;
                dataSheet.Cell(i + 1, 17).Value = selectedOffers[i - 1].ErrorMessage;
            }

            file.SaveAs(newFilePath);
            return newFilePath;
        }

        public string ExportSelectedOffersExcelFile(List<OfferDetailVm> selectedOffers)
        {
            var filePath = Path.Combine(_excelFilesPath, "SelectedOffersData.xlsx");

            var newFilePath = Path.Combine(_excelFilesPath, "SelectedOffersData1.xlsx");

            using var file = new XLWorkbook(filePath);

            var dataSheet = file.Worksheets.FirstOrDefault();

            for (int i = 1; i <= selectedOffers.Count(); i++)
            {
                dataSheet.Cell(i + 1, 1).Value = selectedOffers[i - 1].Id;
                dataSheet.Cell(i + 1, 2).Value = selectedOffers[i - 1].DepartmentName;
                dataSheet.Cell(i + 1, 3).Value = selectedOffers[i - 1].OwnerName;
                dataSheet.Cell(i + 1, 4).Value = selectedOffers[i - 1].Phone1;
                dataSheet.Cell(i + 1, 5).Value = selectedOffers[i - 1].Phone2;
                dataSheet.Cell(i + 1, 6).Value = selectedOffers[i - 1].PurposeName;
                dataSheet.Cell(i + 1, 7).Value = selectedOffers[i - 1].TypeName;
                dataSheet.Cell(i + 1, 8).Value = selectedOffers[i - 1].AreaName;
                dataSheet.Cell(i + 1, 9).Value = selectedOffers[i - 1].LocationName;
                dataSheet.Cell(i + 1, 10).Value = selectedOffers[i - 1].SectionName;
                dataSheet.Cell(i + 1, 11).Value = selectedOffers[i - 1].DistributionName;
                dataSheet.Cell(i + 1, 12).Value = selectedOffers[i - 1].Piece;
                dataSheet.Cell(i + 1, 13).Value = selectedOffers[i - 1].Kasema;
                dataSheet.Cell(i + 1, 14).Value = selectedOffers[i - 1].Street;
                dataSheet.Cell(i + 1, 15).Value = selectedOffers[i - 1].House;
                dataSheet.Cell(i + 1, 16).Value = selectedOffers[i - 1].Price;
                dataSheet.Cell(i + 1, 17).Value = selectedOffers[i - 1].Details;
                dataSheet.Cell(i + 1, 18).Value = selectedOffers[i - 1].CreatedAt;
                dataSheet.Cell(i + 1, 19).Value = selectedOffers[i - 1].CreatedByName;
                dataSheet.Cell(i + 1, 20).Value = selectedOffers[i - 1].ModifiedAt;
                dataSheet.Cell(i + 1, 21).Value = selectedOffers[i - 1].ModifiedByName;
            }

            file.SaveAs(newFilePath);
            return newFilePath;
        }

        public async Task<RespDto<List<UploadedOffersVm>>> GetOffersDataFromFile(IFormFile offersFile)
        {
            // validate uploaded files
            var filePath = await GetStoreValidExcelFilePath(offersFile);
            if(filePath == null || filePath.Data == null || filePath.Error == true) return new RespDto<List<UploadedOffersVm>>() { Error = true, Message = "حدث خطا اثناء قراءه الملف"};
            
            var offers = new List<UploadedOffersVm>();
            using var package = new XLWorkbook(filePath.Data);

            // Get the first worksheet
            var worksheet = package.Worksheets.FirstOrDefault();

            if (worksheet == null) return new RespDto<List<UploadedOffersVm>>().Get400Error("من فضلك ارسل بيانات داخل الملف");

            // Loop through the rows in the worksheet
            for (int row = 2; row <= worksheet.RowsUsed().Count(); row++)
            {
                var result = 0;
                    // Get the cell value
                var offer = new UploadedOffersVm()
                {
                    DepartmentName = worksheet.Cell(row, 1).GetValue<string>(),
                    OwnerName = worksheet.Cell(row, 2).GetValue<string>(),
                    Phone1 = worksheet.Cell(row, 3).GetValue<string>(),
                    Phone2 = worksheet.Cell(row, 4).GetValue<string>(),
                    PurposeName = worksheet.Cell(row, 5).GetValue<string>(),
                    TypeName = worksheet.Cell(row, 6).GetValue<string>(),
                    AreaName = worksheet.Cell(row, 7).GetValue<string>(),
                    LocationName = worksheet.Cell(row, 8).GetValue<string>(),
                    SectionName = worksheet.Cell(row, 9).GetValue<string>(),
                    DistributionName = worksheet.Cell(row, 10).GetValue<string>(),
                    Piece = worksheet.Cell(row, 11).TryGetValue<int>(out result) ? result : 0,
                    Kasema = worksheet.Cell(row, 12).TryGetValue<int>(out result) ? result : 0,
                    Street = worksheet.Cell(row, 13).TryGetValue<int>(out result) ? result : 0,
                    House = worksheet.Cell(row, 14).TryGetValue<int>(out result) ? result : 0,
                    Price = worksheet.Cell(row, 15).TryGetValue<int>(out result) ? result : 0,
                    Details = worksheet.Cell(row, 16).GetValue<string>()
                };

                offers.Add(offer);
            }

            return new RespDto<List<UploadedOffersVm>>() { Data = offers};

        }


        private async Task<RespDto<string>> GetStoreValidExcelFilePath(IFormFile file)
        {
            try
            {
                if (IsFileValid(file))
                {
                    var fileName = Guid.NewGuid().ToString() + file.FileName;

                    var filePath = Path.Combine(_excelFilesPath, "UploadedOffer.xlsx");

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return new RespDto<string>() { Data = filePath };
                }

                return new RespDto<string>() { Data = "", Error = true, Message = "من فضلك ارسل البيانات فى ملف اكسل" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطا اثناء حفظ ملف الاكسل");
                return new RespDto<string>() { Data = null, Error = true, Message = "حدث خطا اثناء حفظ الملف" };
            }

        }

        private bool IsFileValid(IFormFile file)
        {
            var supportedExtensions = new[] { ".xls", ".xlsx" };

            var supportedContentTypes = new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "application/vnd.ms-excel" };


            // check file extension
            if (!supportedExtensions.Contains(Path.GetExtension(file.FileName).ToLower())) return false;

            // check file type
            if (!supportedContentTypes.Contains(file.ContentType)) return false;

            // check file size
            if (file == null || file.Length == 0) return false;

            // check if file contains sheets
            try
            {
                using var package = new XLWorkbook(file.OpenReadStream());
                var worksheet = package.Worksheets.FirstOrDefault();
                if (worksheet == null)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }

    }
}
