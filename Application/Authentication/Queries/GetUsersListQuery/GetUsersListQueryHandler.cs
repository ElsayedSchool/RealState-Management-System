using Application.CategoryApp;
using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.GetUsersListQuery
{
    public class GetUsersListQueryHandler : IRequestHandler<GetAllUsersQuery, List<UsersListVm>>
    {
        private readonly IUserManagerService _userManager;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(IUserManagerService _userManager, IMapper mapper)
        {
            this._userManager = _userManager;
            _mapper = mapper;
        }
        public async Task<RespDto<List<UsersListVm>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.GetAllUsersAsync();
            var resp = _mapper.Map<List<UsersListVm>> (users);
            return new RespDto<List<UsersListVm>>() { Data = resp };
        }
    }
}
