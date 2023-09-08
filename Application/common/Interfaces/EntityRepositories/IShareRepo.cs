using Application.Common.Interfaces.EntityRepositories;
using Application.Common.Models;
using Application.OfferApp.Queries.GetOffersList;
using Application.SharedApp.Queries.GetSharedOffers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.common.Interfaces.EntityRepositories
{
    public interface IShareRepo : IRepository<Share>
    {
        Task<RespDto<OffersQueryResponse>> GetAllSharedOffersByQuery(GetAllSharedOffersQuery query, string userId);
    }
}
