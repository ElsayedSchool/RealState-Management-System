using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.DeactivateUserCommand
{
    public class DeactivateUserCoommandHandler : IRequestHandler<DeactivateUserCommand, bool>
    {
        private readonly IUserManagerService _userManager;

        public DeactivateUserCoommandHandler(IUserManagerService userManager)
        {
            _userManager = userManager;
        }
        public async Task<RespDto<bool>> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userManager.DeactivateUser(request.Id, request.Deactivate);
        }
    }
}
