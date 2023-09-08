using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using Application.OfferApp.Queries.GetOffersList;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FavoriteApp.Queries
{
    public class GetAllFavoritesQueryHandler : IRequestHandler<GetAllFavoritesQuery, OffersQueryResponse>
    {
        private readonly IApplicationRepo _repo;
        private readonly ICurrentUserService _currentUser;

        public GetAllFavoritesQueryHandler(IApplicationRepo repo, ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        public async Task<RespDto<OffersQueryResponse>> Handle(GetAllFavoritesQuery request, CancellationToken cancellationToken)
        {
            return await _repo.FavoriteRepo.GetOffersByQuery(request, _currentUser.UserId);
        }
    }
}
