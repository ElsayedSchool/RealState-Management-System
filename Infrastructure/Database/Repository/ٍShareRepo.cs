using Application.common.Interfaces.EntityRepositories;
using Application.Common.Models;
using Application.FavoriteApp.Queries;
using Application.OfferApp.Queries.GetOffersList;
using Application.SharedApp.Queries.GetSharedOffers;
using Domain.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class ShareRepo : Repository<Share>, IShareRepo
    {
        private readonly ApplicationDbContext _applicationDb;

        public ShareRepo(ApplicationDbContext applicationDb) : base(applicationDb)
        {
            _applicationDb = applicationDb;
        }

        public async Task<RespDto<OffersQueryResponse>> GetAllSharedOffersByQuery(GetAllSharedOffersQuery query,string userId)
        {
            try
            {
                var filter = PredicateBuilder.New<Share>(true); // True for "AND" initial operator

                if (!string.IsNullOrEmpty(query.OwnerName))
                {
                    filter = filter.And(p => p.Offer.OwnerName.Contains(query.OwnerName));
                }

                if (query.IsWatched)
                {
                    filter = filter.And(p => p.IsWatched == true);
                }

                if (!string.IsNullOrEmpty(userId))
                {
                    filter = filter.And(p => p.SharedToId == userId);
                }

                if (!string.IsNullOrEmpty(query.SharedFromId))
                {
                    filter = filter.And(p => p.TransferFromUser.Id == query.SharedFromId);
                }


                if (!string.IsNullOrEmpty(query.Phone1))
                {
                    filter = filter.And(p => p.Offer.Phone1.Contains(query.Phone1));
                }

                if (!string.IsNullOrEmpty(query.Phone2))
                {
                    filter = filter.And(p => p.Offer.Phone2.Contains(query.Phone2));
                }

                if (query.DepartmentId != 0)
                {
                    filter = filter.And(p => p.Offer.DepartmentId == query.DepartmentId);
                }

                if (query.PurposeId != 0)
                {
                    filter = filter.And(p => p.Offer.PurposeId == query.PurposeId);
                }

                if (query.TypeId != 0)
                {
                    filter = filter.And(p => p.Offer.TypeId == query.TypeId);
                }

                if (query.AreaId != 0)
                {
                    filter = filter.And(p => p.Offer.AreaId == query.AreaId);
                }

                if (query.LocationId != 0)
                {
                    filter = filter.And(p => p.Offer.LocationId == query.LocationId);
                }

                if (query.SectionId != 0)
                {
                    filter = filter.And(p => p.Offer.SectionId == query.SectionId);
                }

                if (query.DistributionId != 0)
                {
                    filter = filter.And(p => p.Offer.DistributionId == query.DistributionId);
                }

                if (query.Kasema != 0)
                {
                    filter = filter.And(p => p.Offer.Kasema == query.Kasema);
                }

                if (query.Id != 0)
                {
                    filter = filter.And(p => p.Offer.Id == query.Id);
                }

                if (query.Piece != 0)
                {
                    filter = filter.And(p => p.Offer.Piece == query.Piece);
                }

                if (query.House != 0)
                {
                    filter = filter.And(p => p.Offer.House == query.House);
                }

                if (query.Street != 0)
                {
                    filter = filter.And(p => p.Offer.Street == query.Street);
                }

                if (!string.IsNullOrEmpty(query.Details))
                {
                    filter = filter.And(p => p.Offer.Details.Contains(query.Details));
                }

                var offers = _applicationDb.Shares
                    .AsNoTracking()
                    .Include(p => p.Offer)
                    .ThenInclude(p => p.Department)
                    .Include(p => p.Offer)
                    .ThenInclude(p => p.Purpose)
                    .Include(p => p.Offer)
                    .ThenInclude(p => p.Type)
                    .Include(p => p.Offer)
                    .ThenInclude(p => p.Area)
                    .Include(p => p.Offer)
                    .ThenInclude(p => p.Location)
                    .Include(p => p.TransferFromUser)
                    .Where(filter)
                    .OrderByDescending(p => p.Id)
                    .Select(p => new OffersListVm
                    {
                        Id = p.Offer.Id,
                        DepartmentId = p.Offer.DepartmentId,
                        DepartmentName = p.Offer.Department.Name,
                        PurposeId = p.Offer.PurposeId,
                        PurposeName = p.Offer.Purpose.Name,
                        TypeId = p.Offer.TypeId,
                        TypeName = p.Offer.Type.Name,
                        SharedFromName = p.TransferFromUser.Name,
                        AreaId = p.Offer.AreaId,
                        AreaName = p.Offer.Area == null ? "" : p.Offer.Area.Name,
                        LocationId = p.Offer.LocationId,
                        LocationName = p.Offer.Location == null ? "" : p.Offer.Location.Name
                    });

                var totalCount = await offers.CountAsync();
                var offersList = await offers.Skip(query.Skip ?? 0).Take(query.Take ?? 20).ToListAsync();

                var response = new OffersQueryResponse() { Offers = offersList, TotalCount = totalCount };

                return new RespDto<OffersQueryResponse>() { Data = response };
            }
            catch (Exception ex)
            {
                return new RespDto<OffersQueryResponse>() { Error = true, Message = "حدث خطا اثناء استرجاع العروض!" };
            }
        }
    }
   
}
