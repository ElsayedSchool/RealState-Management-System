using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class SignOutCommandHandler : IRequestHandler<SignOutCommand, bool>
    {

        private readonly IUserSignInManagerService _signInManager;

        public SignOutCommandHandler(IUserSignInManagerService signInManager)
        {
            _signInManager = signInManager;
        }

        
        public async Task<RespDto<bool>> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return new RespDto<bool>() { Data = true };
        }
    }
}
