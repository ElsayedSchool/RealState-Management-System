using Application.common.Interfaces.Files;
using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Commands.DeleteOffers
{
    public class DeleteOffersCommandHandler : IRequestHandler<DeleteOffersCommand, bool>
    {
        private readonly IApplicationRepo _repo;
        private readonly IPhotoFilesService _photoFiles;

        public DeleteOffersCommandHandler(IApplicationRepo repo, IPhotoFilesService photoFiles)
        {
            _repo = repo;
            _photoFiles = photoFiles;
        }

        public async Task<RespDto<bool>> Handle(DeleteOffersCommand request, CancellationToken cancellationToken)
        {
            var DeletedOffers = await _repo.OfferRepo.FindAllAsync(p => request.Offers.Contains(p.Id));
            
            var DeletedOffersImages = await _repo.PhotoRepo.FindAllAsync(p => request.Offers.Contains(p.OfferId));
            
            if (DeletedOffers == null || DeletedOffers.Error == true || DeletedOffers.Data == null) return new RespDto<bool>().Get500Error("حدث خطا اثناء استرجاع بيانات العروض من قاعده البيانات");
            
            if (DeletedOffers.Data.Count() == 0) return new RespDto<bool>() { Data = true };
            
            var resp = await _repo.OfferRepo.DeleteRangeAsync(DeletedOffers.Data);
            
            if (resp == null || resp.Error == true || resp.Data == false) return new RespDto<bool>().Get500Error("حدث خطا اثناء حذف العروض !, برجاء المحاوله لاحقا");

            _photoFiles.RemovePhotoFilesFromServer(DeletedOffersImages?.Data?.Select(p => p.PhotoUrl).ToList() ?? new List<string>());
            
            return resp;

        }
    }

}
