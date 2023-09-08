using Application.common.Interfaces.EntityRepositories;
using Application.common.Interfaces.Files;
using Application.Common.Interfaces;
using Application.Common.Interfaces.EntityRepositories;
using Application.Common.Mediatr;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Commands.UpsertOffer
{
    public class UpsertOfferCommandHandler : IRequestHandler<UpsertOfferCommand, bool>
    {
        private readonly IApplicationRepo _repo;
        private readonly ILogger<UpsertOfferCommand> _logger;
        private readonly IPhotoFilesService _photoFiles;
        private readonly ICurrentUserService _currentUser;

        public UpsertOfferCommandHandler(
                IApplicationRepo applicationRepo, 
                ILogger<UpsertOfferCommand> logger, 
                IPhotoFilesService photoFiles,
                ICurrentUserService currentUser)
        {
            _repo = applicationRepo;
            _logger = logger;
            _photoFiles = photoFiles;
            _currentUser = currentUser;
        }

        public async Task<RespDto<bool>> Handle(UpsertOfferCommand request, CancellationToken cancellationToken)
        {
            // validate categories
             var categories = await _repo.CategoryRepo.GetAllAsync();

            if (categories == null || categories.Error == true) return new RespDto<bool> { Error = true, Message = categories.Message, Data = false };
            
            var isLookupsValid = isValidLookups(categories.Data, request);

            if (!isLookupsValid) return new RespDto<bool>() { Error = true, Message = "من فضلك ارسل بيانات الاختيارات صحيحه!" };
            
            // validate Photos files
            var filesPathes = _photoFiles.GetValidFilesNewNames(request.Photos);

            var isEditing = request.Id == 0 ? false : true;


            Offer offer = null;
            if(isEditing)
            {
                var resp = await _repo.OfferRepo.FindByIdAsync(request.Id);
                if (resp == null || resp.Data == null) return resp.GetNotFoundError(resp.Message);
                offer = resp.Data;
                if (!string.IsNullOrEmpty(request.Comment)) offer.Comments.Add(new Comment() { Date = DateTime.UtcNow, UserId = _currentUser.UserId, Detail = request.Comment });
                offer.ModifiedAt = DateTime.UtcNow;
                offer.ModifiedById = _currentUser.UserId;
            }
            else
            {
                offer = new Offer();
                await _repo.OfferRepo.CreateAsync(offer);
                if (!string.IsNullOrEmpty(request.Comment)) offer.Comments.Add(new Comment() { Date = DateTime.UtcNow, UserId = _currentUser.UserId, Detail = request.Comment });
                offer.CreatedAt = DateTime.UtcNow;
                offer.CreatedById = _currentUser.UserId;
                if (filesPathes.Count > 0)
                {
                    foreach (var fileName in filesPathes)
                    {
                        offer.Photos.Add(new Photo() { PhotoUrl = fileName.Key });
                    }
                }    
            }

            offer.OwnerName = request.OwnerName;
            offer.Phone1 = request.Phone1;
            offer.Phone2 = request.Phone2;
            offer.DepartmentId = request.DepartmentId;
            offer.PurposeId = request.PurposeId;
            offer.TypeId = request.TypeId;
            offer.AreaId = request.AreaId;
            offer.LocationId = request.LocationId;
            offer.SectionId = request.SectionId;
            offer.DistributionId = request.DistributionId;
            offer.Piece = request.Piece;
            offer.Price = request.Price;
            offer.Details = request.Details;
            offer.House = request.House;
            offer.Kasema = request.Kasema;
            offer.Street = request.Street;
            await _repo.Commit();

            if (!isEditing) await _photoFiles.StoreValidPhotoFilesOnServer(filesPathes);
            return new RespDto<bool>() { Data = true };
        }

        private bool isValidLookups(List<Category> categories, UpsertOfferCommand offer)
        {
            bool isDepartValid = categories.Exists(p=> p.Id == offer.DepartmentId && p.category == AdsCategories.department);
            bool isPurposeValid = categories.Exists(p=> p.Id == offer.PurposeId && p.category == AdsCategories.purpose);
            bool isTypeValid = categories.Exists(p=> p.Id == offer.TypeId && p.category == AdsCategories.type);
            bool isAreaValid = categories.Exists(p=> p.Id == offer.AreaId && p.category == AdsCategories.area);
            bool isLocatValid = categories.Exists(p=> p.Id == offer.LocationId && p.category == AdsCategories.location);
            bool isSectionValid = categories.Exists(p=> p.Id == offer.SectionId && p.category == AdsCategories.section);
            bool isDistributionValid = categories.Exists(p=> p.Id == offer.DistributionId && p.category == AdsCategories.distribution);
            
            if(isDepartValid && isPurposeValid && isAreaValid && isLocatValid && isSectionValid && isDistributionValid && isTypeValid) { 
                return true;
            }
            return false;
        }
    }
}
