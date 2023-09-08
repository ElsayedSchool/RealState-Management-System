using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.RemoveUserCommand
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, bool>
    {
        private readonly IUserManagerService _userManager;

        public RemoveUserCommandHandler(IUserManagerService userManager)
        {
            _userManager = userManager;
        }
        public async Task<RespDto<bool>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            return await _userManager.DeleteUserAsync(request.Id);
        }
    }
}
