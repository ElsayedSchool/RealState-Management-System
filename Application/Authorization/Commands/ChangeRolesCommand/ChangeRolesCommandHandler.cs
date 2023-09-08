using Application.common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Commands.ChangeRolesCommand
{
    public class ChangeRolesCommandHandler : IRequestHandler<ChangeRolesCommand, bool>
    {
        private readonly IUserRoleManagerService _roleManager;

        public ChangeRolesCommandHandler(IUserRoleManagerService roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<RespDto<bool>> Handle(ChangeRolesCommand request, CancellationToken cancellationToken)
        {
            return await _roleManager.ChangeUserRolesAsync(request.Id, request.Roles);
        }
    }
}
