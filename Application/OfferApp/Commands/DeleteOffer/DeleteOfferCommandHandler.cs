using Application.common.Interfaces.Files;
using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Commands.DeleteOffer
{
    public class DeleteOfferCommandHandler : IRequestHandler<DeleteOfferCommand, bool>
    {
        private readonly IApplicationRepo _repo;
        private readonly IPhotoFilesService _photoFiles;

        public DeleteOfferCommandHandler(IApplicationRepo repo, IPhotoFilesService photoFiles)
        {
            _repo = repo;
            _photoFiles = photoFiles;
        }

        public async Task<RespDto<bool>> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            var offerImages = await _repo.PhotoRepo.FindAllAsync(p => p.OfferId == request.Id);

            if (offerImages is null || offerImages.Error == true || offerImages.Data == null) return new RespDto<bool>().Get500Error("حدث خطا اثناء استرجاع الصور لحذفها!, برجاء المحاوله لاحقا");
            

            var resp = await _repo.OfferRepo.DeleteAsync(request.Id);

            if (resp is null || resp.Error == true) return new RespDto<bool>().Get500Error("حدث خطا اثناء حذف العرض !, برجاء المحاوله لاحقا");

            var photoResp = _photoFiles.RemovePhotoFilesFromServer(offerImages.Data.Select(p => p.PhotoUrl).ToList());

            return resp;
        }
    }
}
