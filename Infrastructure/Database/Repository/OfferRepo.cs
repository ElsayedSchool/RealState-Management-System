using Application.CommentApp.Query.GetAllCommentsQuery;
using Application.common.Interfaces.EntityRepositories;
using Application.Common.Models;
using Application.OfferApp.Commands.ChangeDepartment;
using Application.OfferApp.Queries.GetOfferDetail;
using Application.OfferApp.Queries.GetOffersList;
using Domain.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class OfferRepo : Repository<Offer>, IOfferRepo
    {
        private readonly ApplicationDbContext _applicationDb;

        public OfferRepo(ApplicationDbContext applicationDb ) : base(applicationDb)
        {
            _applicationDb = applicationDb;
        }

        public async Task<RespDto<OfferDetailVm>> GetOfferDetail(int Id)
        {
            try
            {
                var resp = await _applicationDb
                    .Offers
                    .Include(p => p.Department)
                    .Include(p => p.Purpose)
                    .Include(p => p.Type)
                    .Include(p => p.Area)
                    .Include(p => p.Location)
                    .Include(p => p.Section)
                    .Include(p => p.Distribution)
                    .Include(p => p.Photos)
                    .Include(p => p.CreatedBy)
                    .Include(p => p.ModifiedBy)
                    .Where(p => p.Id == Id)
                    .Select(p => new OfferDetailVm()
                    {
                        Id = p.Id,
                        OwnerName = p.OwnerName,
                        Phone1 = p.Phone1,
                        Phone2 = p.Phone2,
                        DepartmentId = p.DepartmentId,
                        DepartmentName = p.DistributionId != null ? p.Department.Name : "",
                        PurposeId = p.PurposeId,
                        PurposeName = p.DistributionId != null ? p.Purpose.Name : "",
                        TypeId = p.TypeId,
                        TypeName = p.DistributionId != null ? p.Type.Name : "",
                        AreaId = p.AreaId,
                        AreaName = p.DistributionId != null ? p.Area.Name : "",
                        LocationId = p.LocationId,
                        LocationName = p.DistributionId != null ? p.Location.Name : "",
                        SectionId = p.SectionId,
                        SectionName = p.DistributionId != null ? p.Section.Name : "",
                        DistributionId = p.DistributionId,
                        DistributionName = p.DistributionId != null ? p.Distribution.Name : "",
                        Price = p.Price,
                        Kasema = p.Kasema,
                        House = p.House,
                        Street = p.Street,
                        Details = p.Details,
                        CreatedByName = p.CreatedBy.Name,
                        CreatedAt = p.CreatedAt,
                        ModifiedByName = string.IsNullOrEmpty(p.ModifiedById) ? p.ModifiedBy.Name : "",
                        ModifiedAt = p.ModifiedAt,
                        Photos = p.Photos
                    })
                    .FirstOrDefaultAsync();


                return new RespDto<OfferDetailVm>() { Data = resp };
            }
            catch (Exception ex)
            {
                return new RespDto<OfferDetailVm>() { Error = true, Message = "حدث خطا اثناء استرجاع تفاصيل العرض!" };
            }
        }

        public async Task<RespDto<OffersQueryResponse>> GetOffersByQuery(GetAllOffersQuery query)
        {
            try
            {
                var filter = PredicateBuilder.New<Offer>(true); // True for "AND" initial operator

                if (!string.IsNullOrEmpty(query.OwnerName))
                {
                    filter = filter.And(p => p.OwnerName.Contains(query.OwnerName));
                }

                if (!string.IsNullOrEmpty(query.Phone1))
                {
                    filter = filter.And(p => p.Phone1.Contains(query.Phone1));
                }

                if (!string.IsNullOrEmpty(query.Phone2))
                {
                    filter = filter.And(p => p.Phone2.Contains(query.Phone2));
                }

                if (query.DepartmentId != 0)
                {
                    filter = filter.And(p => p.DepartmentId == query.DepartmentId);
                }

                if (query.PurposeId != 0)
                {
                    filter = filter.And(p => p.PurposeId == query.PurposeId);
                }

                if (query.TypeId != 0)
                {
                    filter = filter.And(p => p.TypeId == query.TypeId);
                }

                if (query.AreaId != 0)
                {
                    filter = filter.And(p => p.AreaId == query.AreaId);
                }

                if (query.LocationId != 0)
                {
                    filter = filter.And(p => p.LocationId == query.LocationId);
                }

                if (query.SectionId != 0)
                {
                    filter = filter.And(p => p.SectionId == query.SectionId);
                }

                if (query.DistributionId != 0)
                {
                    filter = filter.And(p => p.DistributionId == query.DistributionId);
                }

                if (query.Kasema != 0)
                {
                    filter = filter.And(p => p.Kasema == query.Kasema);
                }

                if (query.Id != 0)
                {
                    filter = filter.And(p => p.Id == query.Id);
                }

                if (query.Piece != 0)
                {
                    filter = filter.And(p => p.Piece == query.Piece);
                }

                if (query.House != 0)
                {
                    filter = filter.And(p => p.House == query.House);
                }

                if (query.Street != 0)
                {
                    filter = filter.And(p => p.Street == query.Street);
                }

                if (!string.IsNullOrEmpty(query.Details))
                {
                    filter = filter.And(p => p.Details.Contains(query.Details));
                }

                var offers = _applicationDb.Offers
                    .AsNoTracking()
                    .Include(p => p.Department)
                    .Include(p => p.Purpose)
                    .Include(p => p.Type)
                    .Include(p => p.Area)
                    .Include(p => p.Location)
                    .Where(filter)
                    .OrderByDescending(p => p.Id)
                    .Select(p => new OffersListVm
                    {
                        Id = p.Id,
                        DepartmentId = p.DepartmentId,
                        DepartmentName = p.Department.Name,
                        PurposeId = p.PurposeId,
                        PurposeName = p.Purpose.Name,
                        TypeId = p.TypeId,
                        TypeName = p.Type.Name,
                        AreaId = p.AreaId,
                        AreaName = p.Area == null ? "" : p.Area.Name,
                        LocationId = p.LocationId,
                        LocationName = p.Location == null ? "" : p.Location.Name
                    }).AsQueryable();

                var totalCount = await offers.CountAsync();
                var offersList = await offers.Skip(query.Skip ?? 0).Take(query.Take ?? 20).ToListAsync();

                var response = new OffersQueryResponse() { Offers =  offersList, TotalCount = totalCount };


                return new RespDto<OffersQueryResponse>() { Data = response };
            }
            catch (Exception ex)
            {
                return new RespDto<OffersQueryResponse>() { Error = true, Message = "حدث خطا اثناء استرجاع العروض!" };
            }
        }


        public async Task<RespDto<bool>> UpdateOffersDepartment(ChangeDepartmentCommand command)
        {
            try
            {
                var resp = await _applicationDb
                    .Offers
                    .Where(p => command.Offers.Contains(p.Id))
                    .ExecuteUpdateAsync(c => c.SetProperty(u => u.DepartmentId, command.NewDepartmentId));
                await _applicationDb.SaveChangesAsync();
                return new RespDto<bool>() { Data = true };
            }
            catch (Exception ex)
            {

                return new RespDto<bool>().Get500Error("حدث خطا اثناء تحديث بيانات العروض, برجاء اعاده المحاوله");
            }
            
        }

        public async Task<RespDto<List<OfferDetailVm>>> GetSelectedOffersDetails(List<int> selectedOffers)
        {
            try
            {
                var resp = await _applicationDb
                    .Offers
                    .Include(p => p.Department)
                    .Include(p => p.Purpose)
                    .Include(p => p.Type)
                    .Include(p => p.Area)
                    .Include(p => p.Location)
                    .Include(p => p.Section)
                    .Include(p => p.Distribution)
                    .Include(p => p.Photos)
                    .Include(p => p.CreatedBy)
                    .Include(p => p.ModifiedBy)
                    .Where(p => selectedOffers.Contains(p.Id))
                    .Select(p => new OfferDetailVm()
                    {
                        Id = p.Id,
                        OwnerName = p.OwnerName,
                        Phone1 = p.Phone1,
                        Phone2 = p.Phone2,
                        DepartmentId = p.DepartmentId,
                        DepartmentName = p.DistributionId != null ? p.Department.Name : "",
                        PurposeId = p.PurposeId,
                        PurposeName = p.DistributionId != null ? p.Purpose.Name : "",
                        TypeId = p.TypeId,
                        TypeName = p.DistributionId != null ? p.Type.Name : "",
                        AreaId = p.AreaId,
                        AreaName = p.DistributionId != null ? p.Area.Name : "",
                        LocationId = p.LocationId,
                        LocationName = p.DistributionId != null ? p.Location.Name : "",
                        SectionId = p.SectionId,
                        SectionName = p.DistributionId != null ? p.Section.Name : "",
                        DistributionId = p.DistributionId,
                        DistributionName = p.DistributionId != null ? p.Distribution.Name : "",
                        Price = p.Price,
                        Kasema = p.Kasema,
                        House = p.House,
                        Street = p.Street,
                        Details = p.Details,
                        CreatedByName = p.CreatedBy.Name,
                        CreatedAt = p.CreatedAt,
                        ModifiedByName = string.IsNullOrEmpty(p.ModifiedById) ? p.ModifiedBy.Name : "",
                        ModifiedAt = p.ModifiedAt,
                        Photos = p.Photos
                    })
                    .ToListAsync();


                return new RespDto<List<OfferDetailVm>>() { Data = resp };
            }
            catch (Exception ex)
            {
                return new RespDto<List<OfferDetailVm>>() { Error = true, Message = "حدث خطا اثناء استرجاع تفاصيل العرض!" };
            }
        }
    }
 
}
