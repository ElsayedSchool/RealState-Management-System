using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.Commands.AddSharedOffers
{
    public class AddSharedOffersCommandHandler : IRequestHandler<AddSharedOffersCommand, bool>
    {
        private readonly IApplicationRepo _repo;
        private readonly ICurrentUserService _currentUser;

        public AddSharedOffersCommandHandler(IApplicationRepo repo, ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        public async Task<RespDto<bool>> Handle(AddSharedOffersCommand request, CancellationToken cancellationToken)
        {
            List<Share> shares = new List<Share>();
            request.SharedToList.ForEach(p =>
            {
                request.Offers.ForEach(o =>
                {
                    shares.Add(new Share { OfferId = o, SharedToId = p, SharedFromId = _currentUser.UserId, Date = DateTime.UtcNow  });
                });
            });

            var resp = await _repo.ShareRepo.CreateListAsync(shares);
            if (resp == null || resp.Error == true) return new RespDto<bool>().Get500Error("حدث خطا اثناء حفظ بيانات المشاركه");
            await _repo.Commit();
            return resp;
        }
    }
}
