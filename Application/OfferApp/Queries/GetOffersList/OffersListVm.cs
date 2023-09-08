using Application.CategoryApp;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Queries.GetOffersList
{
    public class OffersListVm : IMapFrom<Offer>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Offer, OffersListVm>()
                .ForMember(s => s.DepartmentName, opt => opt.MapFrom(d => d.Department != null ? d.Department.Name : ""))
                .ForMember(s => s.PurposeName, opt => opt.MapFrom(d => d.Purpose != null ? d.Purpose.Name : ""))
                .ForMember(s => s.TypeName, opt => opt.MapFrom(d => d.Type != null ? d.Type.Name : ""));
        }
        public int Id { get; set; }

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

        public string? SharedFromName { get; set; }
    }
}
