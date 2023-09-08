using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.ResetPasswordCommand
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IUserManagerService _userManager;

        public ResetPasswordCommandHandler(IUserManagerService userManager)
        {
            _userManager = userManager;
        }
        public async Task<RespDto<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userManager.ResetPassword(request.Id);
        }
    }
}
