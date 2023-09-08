using Application.common.Interfaces.Files;
using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using Application.OfferApp.Queries.GetOfferDetail;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileApp.Commands.AddUploadedOffers
{
    public class _ِAddUploadedOffersCommandHandler : IRequestHandler<AddUploadedOffersCommand, string>
    {
        private readonly IApplicationRepo _repo;
        private readonly IExcelFileService _fileService;
        private readonly ICurrentUserService _currentUser;

        public _ِAddUploadedOffersCommandHandler(IApplicationRepo repo, IExcelFileService fileService, ICurrentUserService currentUser )
        {
            _repo = repo;
            _fileService = fileService;
            _currentUser = currentUser;
        }

        public async Task<RespDto<string>> Handle(AddUploadedOffersCommand request, CancellationToken cancellationToken)
        {
            // extract data from file
            var newOffers = await _fileService.GetOffersDataFromFile(request.NewOffersFile);

            if(newOffers == null || newOffers.Error == true || newOffers.Data == null) return new RespDto<string>() { Error = true, Message = "حدث خطا اثناء قراءه الملف"};

            // validate data and construct valid and invalid list
            var lookups = await _repo.CategoryRepo.GetAllAsync();

            if (lookups == null || lookups.Error == true || lookups.Data == null) return new RespDto<string>() { Error = true, Message = "حدث خطا اثناء استرجاع الخيارات برجاء المحاوله لاحقا" };

            var validatedOffers = GetValidOffersData(newOffers.Data, lookups.Data);

            // add valid data to the database
            var validOffers = validatedOffers.Where(p => string.IsNullOrEmpty(p.ErrorMessage)).ToList();
            if(validOffers.Count() > 0)
            {
                var mappedOffers = GetMappedOffers(validatedOffers);
                var newOffersResp = await _repo.OfferRepo.CreateListAsync(mappedOffers);
            }

            var invalidOffers = validatedOffers.Where(p => !string.IsNullOrEmpty(p.ErrorMessage)).ToList();

            if (invalidOffers.Count() > 0)
            {
                var path = _fileService.ExportinvalidOffersExcelFile(invalidOffers);
                return new RespDto<string>() { Data = path };
            }
            // return invalid data to the user
            return new RespDto<string>() { Data = "" };
        }

        private List<UploadedOffersVm> GetValidOffersData(List<UploadedOffersVm> newOffers, List<Category> lookups)
        {
            var departs = lookups.Where(p => p.category == AdsCategories.department).ToList();
            var purposes = lookups.Where(p => p.category == AdsCategories.purpose).ToList();
            var types = lookups.Where(p => p.category == AdsCategories.type).ToList();
            var areas = lookups.Where(p => p.category == AdsCategories.area).ToList();
            var locations = lookups.Where(p => p.category == AdsCategories.location).ToList();
            var sections = lookups.Where(p => p.category == AdsCategories.section).ToList();
            var distribution = lookups.Where(p => p.category == AdsCategories.distribution).ToList();


            var validatedOffers = new List<UploadedOffersVm>();

            foreach (var offer in newOffers) 
            {
                string invalidMessage = string.Empty;
                if(departs.FirstOrDefault(p => p.Name == offer.DepartmentName) == null)
                {
                    invalidMessage += "القسم ";
                }
                else
                {
                    offer.DepartmentId = departs.FirstOrDefault(p => p.Name == offer.DepartmentName).Id;
                }

                if (purposes.FirstOrDefault(p => p.Name == offer.PurposeName) == null)
                {
                    invalidMessage += "العمليه ";
                }
                else
                {
                    offer.PurposeId = purposes.FirstOrDefault(p => p.Name == offer.PurposeName).Id;
                }

                if (types.FirstOrDefault(p => p.Name == offer.TypeName) == null)
                {
                    invalidMessage += "نوع العقار ";
                }
                else
                {
                    offer.TypeId = types.FirstOrDefault(p => p.Name == offer.TypeName).Id;
                }

                if (areas.FirstOrDefault(p => p.Name == offer.AreaName) == null)
                {
                    invalidMessage += "المنطقه ";
                }
                else
                {
                    offer.AreaId = areas.FirstOrDefault(p => p.Name == offer.AreaName).Id;
                }

                if (locations.FirstOrDefault(p => p.Name == offer.LocationName) == null)
                {
                    offer.LocationId = null;
                }
                else
                {
                    offer.LocationId = locations.FirstOrDefault(p => p.Name == offer.LocationName).Id;
                }

                if (sections.FirstOrDefault(p => p.Name == offer.SectionName) == null)
                {
                    offer.SectionId = null;
                }
                else
                {
                    offer.SectionId = sections.FirstOrDefault(p => p.Name == offer.SectionName).Id;
                }

                if (distribution.FirstOrDefault(p => p.Name == offer.DistributionName) == null)
                {
                    offer.DistributionId = null;
                }
                else
                {
                    offer.DistributionId = distribution.FirstOrDefault(p => p.Name == offer.DistributionName).Id;
                }

                if (string.IsNullOrEmpty(invalidMessage))
                {
                    validatedOffers.Add(offer);
                }else
                {

                    offer.ErrorMessage = invalidMessage + " غير موجودين بقاعده البيانات";
                    validatedOffers.Add(offer);
                }
            }

            return validatedOffers;
        }

        private List<Offer> GetMappedOffers(List<UploadedOffersVm> offers)
        {
            var offersList = new List<Offer>();
            foreach (var offer in offers)
            {
                var offerDetailVm = new Offer() 
                {
                    OwnerName = offer.OwnerName,
                    Phone1 = offer.Phone1,
                    Phone2 = offer.Phone2,
                    DepartmentId = offer.DepartmentId,
                    PurposeId = offer.PurposeId,
                    TypeId = offer.TypeId,
                    LocationId = offer.LocationId,
                    AreaId  = offer.AreaId,
                    SectionId   = offer.SectionId,
                    DistributionId  = offer.DistributionId,
                    Piece = offer.Piece,
                    Kasema  = offer.Kasema,
                    Street = offer.Street,
                    House = offer.House,
                    Details = offer.Details,
                    CreatedAt = DateTime.UtcNow,
                    CreatedById = _currentUser.UserId,
                };
                offersList.Add(offerDetailVm);
            }
            return offersList;
        }
    }
}
