using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IUserManagerService _userManager;

        public ChangePasswordCommandHandler(IUserManagerService userManager)
        {
            _userManager = userManager;
        }


        public async Task<RespDto<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await isValidUser(request);
            if(user == null || user.Error == true)
                return new RespDto<bool>() { Error = user.Error, Message = user.Message, StatusCode = user.StatusCode};
            var result = await _userManager.ChangeUserPassword(user.Data, request.CurrentPassword, request.NewPassword);
            return result;
        }



        private async Task<RespDto<ApplicationUser>> isValidUser(ChangePasswordCommand request)
        {
            var user = await _userManager.GetUserById(request.UserId);
            if (user == null)
            {
                return new RespDto<ApplicationUser>().Get400Error("This userData is not exist.");
            }
            // check if password is ok
            var isPasswordValid = await _userManager.isPasswordValid(user, request.CurrentPassword);
            if (isPasswordValid == null)
            {
                return new RespDto<ApplicationUser>().Get400Error("Old Password is not valid");
            }
            if(request.isPasswordMatching() == false)
                return new RespDto<ApplicationUser>().Get400Error("New Passwords are not matching.");

            return new RespDto<ApplicationUser>() { Data = user };
        }
    }
}
