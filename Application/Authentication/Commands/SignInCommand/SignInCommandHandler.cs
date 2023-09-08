using Application.Authentication;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.Extensions.Options;


namespace Application.Authentication
{
    public class AddCenterCommandHandler : IRequestHandler<SignInCommand, string>
    {
        private readonly IUserManagerService _userManager;
        private readonly IUserRoleManagerService _userRoleManager;
        private readonly IOptions<AppSettingModel> _option;
        private readonly IJWTToken _token;

        public AddCenterCommandHandler(
            IUserManagerService userManager,
            IUserRoleManagerService userRoleManager,
            IOptions<AppSettingModel> option,
            IJWTToken token)
        {
            _userManager = userManager;
            _userRoleManager = userRoleManager;
            _option = option;
            _token = token;
        }

        public async Task<RespDto<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            // check if username is exist
            var user = await isValidUser(request);
            if(user == null || user.Error == true)
            {
                return new RespDto<string>() { StatusCode = 401, Error = true, Message = user.Message };
            }

            // GetToken object Required by Generate token function
            var tokenData = await GetTokenRequiredData(user.Data);

            // getToken string value
            var tokenValue = _token.GenerateToken(tokenData);

            return new RespDto<string>() { Data = tokenValue };
        }

        private async Task<RespDto<ApplicationUser>> isValidUser(SignInCommand request)
        {
            
            var user = await _userManager.GetUserByNameAsync(request.Username);
            if (user == null)
            {
                return new RespDto<ApplicationUser>().Get400Error("Invalid username or Password.");
            }
            var isLockedOut = await _userManager.IsLockedOut(user);
            if (isLockedOut) return new RespDto<ApplicationUser>() { Error = true, Message = "you are not allowed to login, please contact admin for more details" };
            // check if password is ok
            var isPasswordValid = await _userManager.isPasswordValid(user, request.Password);
            if (isPasswordValid == null || isPasswordValid.Error == true)
            {
                return new RespDto<ApplicationUser>().Get400Error("Invalid username or Password.");
            }
            return new RespDto<ApplicationUser>() { Data = user };
        }

        private async Task<TokenData> GetTokenRequiredData(ApplicationUser user)
        {
            var userRole = await _userRoleManager.GetUserRolesAsync(user.Id);
            var tokenData = new TokenData() { Name = user.UserName, Roles = userRole.Data, UserId = user.Id, SecurityKey = _option.Value.Secret };
            return tokenData;
        }
    }
}
