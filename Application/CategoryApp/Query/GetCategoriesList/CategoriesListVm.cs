using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CategoryApp
{
    public class CategoriesListVm : IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AdsCategories category { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoriesListVm>()
                .ForMember(s => s.Id, opt => opt.MapFrom(d => d.Id));
        }
    }
}
