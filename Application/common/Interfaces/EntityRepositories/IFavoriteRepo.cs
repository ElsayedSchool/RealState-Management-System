using Application.Common.Interfaces.EntityRepositories;
using Application.Common.Models;
using Application.FavoriteApp.Queries;
using Application.OfferApp.Queries.GetOffersList;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.common.Interfaces.EntityRepositories
{
    public interface IFavoriteRepo : IRepository<Favorite>
    {
        Task<RespDto<OffersQueryResponse>> GetOffersByQuery(GetAllFavoritesQuery query, string userId);
    }
}
