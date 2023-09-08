using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.UserIdentity.UserResponseModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Infrastructure.UserIdentity.UserManagerApp
{
    public class UserManagerService : IUserManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<AppSettingModel> _options;

        public UserManagerService(UserManager<ApplicationUser> userManager, IOptions<AppSettingModel> options)
        {
            _userManager = userManager;
            _options = options;
        }

        // create new User
        public async Task<RespDto<bool>> CreateUserAsync(string userName, string password, string Name)
        {

            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
                Name = Name
            };

            var result = await _userManager.CreateAsync(user, password);
            return result.ToApplicationResult();
        }


        public async Task<IList<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.GetUsersInRoleAsync(AppRoles.User);
        }

        public async Task<bool> IsLockedOut(ApplicationUser user)
        {
            return await _userManager.IsLockedOutAsync(user);
        }


        public async Task<ApplicationUser?> GetUserById(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }


        public async Task<ApplicationUser?> GetUserByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }


        public async Task<RespDto<bool>> DeleteUserAsync(string userId)
        {
            var user = await GetUserById(userId);
            if(user == null) {
                return new RespDto<bool>() { Data = true };
            }
            var resp = await _userManager.DeleteAsync(user);
            if(resp.Succeeded) { return new RespDto<bool>() { Data = true }; }
            return new RespDto<bool>() { Data = false, Error = true, Message = "An eroror Occured, please try again" };
        }

        public async Task<RespDto<bool>> ChangeUserPassword(ApplicationUser signInUser, string oldPassword, string newPassword)
        {
            var changePasswordStatus = await _userManager.ChangePasswordAsync(signInUser, oldPassword, newPassword);
            return changePasswordStatus.ToApplicationResult();
        }


        public async Task<RespDto<bool>> isPasswordValid(ApplicationUser signInUser, string password)
        {
            var user = await _userManager.CheckPasswordAsync(signInUser, password);
            if (user == false)
            {
                return new RespDto<bool>() { Error = true, Message = "username or Password is incorrect" };
            }
            return new RespDto<bool>() { Data = true };
        }

        public async Task<RespDto<bool>> DeactivateUser(string userId, bool Deactivate)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null) { return new RespDto<bool>() { Data=false, Error= true, Message= "Send Valid Data" }; }

            /*var lockoutResp = await _userManager.SetLockoutEnabledAsync(user, Deactivate);*/

            var LockoutDateResp = await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddYears(Deactivate ? 10 : -1));
            
            if(LockoutDateResp.Succeeded)
            {
                return new RespDto<bool>() { Data = true };
            }
            return new RespDto<bool>() { Data = false, Error = true, Message= "An Error Occured ,Please Try Again" };           
        }

        public async Task<RespDto<bool>> ResetPassword(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new RespDto<bool>() { Error = true, Message = "Please Send A vaild User", Data = false };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            if(token == null)
            {
                return new RespDto<bool>() { Data = false, Error = true, Message= "User isnot Valid" };
            }
            var resetPasswordTooken = await _userManager.ResetPasswordAsync(user, token, _options.Value.AdminPassword);
            if(!resetPasswordTooken.Succeeded) 
            {
                return new RespDto<bool>() { Data = false, Message = "Operation Failed, pLease try again" };
            }
            return new RespDto<bool>() { Data = true };
        }



        public async Task<RespDto<bool>> HasAny()
        {
            var isExist = _userManager.Users.Any();
            return new RespDto<bool>() { Data = isExist };
        }

    }
}
