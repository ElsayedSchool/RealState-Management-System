using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;

namespace Application.Authorization.Queries.GetRolesQuery
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, IList<string>>
    {
        private readonly IUserRoleManagerService _roleManager;

        public GetUserRolesQueryHandler(IUserRoleManagerService roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<RespDto<IList<string>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleManager.GetUserRolesAsync(request.userId);
        }
    }
}
