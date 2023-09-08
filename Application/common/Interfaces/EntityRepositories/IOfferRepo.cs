using Application.Common.Interfaces.EntityRepositories;
using Application.Common.Models;
using Application.OfferApp.Commands.ChangeDepartment;
using Application.OfferApp.Queries.GetOfferDetail;
using Application.OfferApp.Queries.GetOffersList;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.common.Interfaces.EntityRepositories
{
    public interface IOfferRepo : IRepository<Offer>
    {
        Task<RespDto<OfferDetailVm>> GetOfferDetail(int id);
        Task<RespDto<OffersQueryResponse>> GetOffersByQuery(GetAllOffersQuery query);
        Task<RespDto<bool>> UpdateOffersDepartment(ChangeDepartmentCommand command);
        Task<RespDto<List<OfferDetailVm>>> GetSelectedOffersDetails(List<int> selectedOffers);
    }
}
