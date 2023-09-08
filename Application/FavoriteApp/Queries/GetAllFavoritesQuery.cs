using Application.Common.Mediatr;
using Application.OfferApp.Queries.GetOffersList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FavoriteApp.Queries
{
    public class GetAllFavoritesQuery : IRequestWrapper<OffersQueryResponse>
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }


        public int Piece { get; set; }

        public int Kasema { get; set; }

        public int Street { get; set; }

        public int House { get; set; }

        public int Price { get; set; }

        public string Details { get; set; }

        public int DepartmentId { get; set; }

        public int PurposeId { get; set; }

        public int TypeId { get; set; }

        public int? AreaId { get; set; }

        public int? LocationId { get; set; }

        public int? SectionId { get; set; }

        public int? DistributionId { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
