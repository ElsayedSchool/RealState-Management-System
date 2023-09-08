using Application.CategoryApp;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.GetUsersListQuery
{
    public class UsersListVm:IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, UsersListVm>()
                .ForMember(s => s.Phone, opt => opt.MapFrom(d => d.PhoneNumber))
                .ForMember(s => s.Email, opt => opt.MapFrom(d => d.UserName))
                .ForMember(s => s.IsActive, opt => opt.MapFrom(d => d.LockoutEnd < DateTime.Now || d.LockoutEnd == null));
        }

    }
}
