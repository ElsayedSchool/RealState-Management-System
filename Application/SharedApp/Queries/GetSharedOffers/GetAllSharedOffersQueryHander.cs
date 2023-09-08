using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using Application.FavoriteApp.Queries;
using Application.OfferApp.Queries.GetOffersList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.Queries.GetSharedOffers
{
    public class GetAllSharedOffersQueryHander : IRequestHandler<GetAllSharedOffersQuery, OffersQueryResponse>
    {
        private readonly IApplicationRepo _repo;
        private readonly ICurrentUserService _currentUser;

        public GetAllSharedOffersQueryHander(IApplicationRepo repo,ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        public async Task<RespDto<OffersQueryResponse>> Handle(GetAllSharedOffersQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ShareRepo.GetAllSharedOffersByQuery(request, _currentUser.UserId);
        }
    }
}
