using Application.Common.Mappings;
using Application.OfferApp.Queries.GetOffersList;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.Queries
{
    public class AllUsersVm :IMapFrom<User>
    {

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, AllUsersVm>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
