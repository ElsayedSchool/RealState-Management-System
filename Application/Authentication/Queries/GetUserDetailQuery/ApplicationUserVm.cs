using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.GetUserDetailQuery
{
    public class ApplicationUserVm : IMapFrom<ApplicationUser>
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, ApplicationUserVm>()
                .ForMember(x => x.UserId, c => c.MapFrom(x => x.Id))
                .ReverseMap();

        }
    }
}
