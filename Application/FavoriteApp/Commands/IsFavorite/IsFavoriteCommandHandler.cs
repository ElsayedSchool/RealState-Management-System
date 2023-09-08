using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FavoriteApp.Commands.IsFavorite
{
    public class IsFavoriteCommandHandler : IRequestHandler<IsFavoriteCommand, bool>
    {
        private readonly IApplicationRepo _repo;
        private readonly ICurrentUserService _currentUser;

        public IsFavoriteCommandHandler(IApplicationRepo repo, ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }
        public async Task<RespDto<bool>> Handle(IsFavoriteCommand request, CancellationToken cancellationToken)
        {
            var resp = await _repo.FavoriteRepo.FindAllAsync(p => p.OfferId == request.offerId && _currentUser.UserId == p.UserId);
            if (resp == null || resp.Data == null) return new RespDto<bool>().Get500Error("حدث خطا اثناء التحقق من المفضلات من فضلك اعد تحميل الصفحه");
            return new RespDto<bool>() { Data = resp.Data.Count() > 0 };
        }
    }
}
