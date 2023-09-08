using Application.CommentApp.Query.GetAllCommentsQuery;
using Application.Common.Mappings;
using Application.OfferApp.Queries.GetOffersList;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Queries.GetOfferDetail
{
    public class OfferDetailVm
    {
        public int Id { get; set; }

        public string OwnerName { get; set; } = string.Empty;
        public string Phone1 { get; set; } = string.Empty;
        public string? Phone2 { get; set; }


        public int Piece { get; set; }

        public int Kasema { get; set; }

        public int Street { get; set; }

        public int House { get; set; }

        public int Price { get; set; }

        public string Details { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        public int PurposeId { get; set; }
        public string PurposeName { get; set; } = string.Empty;

        public int TypeId { get; set; }
        public string TypeName { get; set; } = string.Empty;

        public int? AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;

        public int? LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;

        public int? SectionId { get; set; }
        public string SectionName { get; set; } = string.Empty;

        public int? DistributionId { get; set; }
        public string DistributionName { get; set; } = string.Empty;

        public string CreatedByName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string ModifiedByName { get; set; } = string.Empty;

        public DateTime? ModifiedAt { get; set; }

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
