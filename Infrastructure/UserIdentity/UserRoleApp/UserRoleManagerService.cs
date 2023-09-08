using Application.common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.UserIdentity.UserResponseModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UserIdentity.UserRoleApp
{
    public class UserRoleManagerService : IUserRoleManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRoleManagerService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<RespDto<IList<string>>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
            {
                return new RespDto<IList<string>>() { Error = true, Message = "Please Send a valid User Data" };
            }

            var roles =  await _userManager.GetRolesAsync(user);
            return new RespDto<IList<string>>() { Data = roles };
        }

        public async Task<RespDto<bool>> AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            var resp = await _userManager.AddToRoleAsync(user, roleName);
            return new RespDto<bool>() { Data = resp.Succeeded };
        }


        public async Task<RespDto<bool>> ChangeUserRolesAsync(string userId, List<string> updatedRoles)
        {
            try
            {

                var userRoles = await GetUserRolesAsync(userId);

                var user = await _userManager.FindByIdAsync(userId);

                if (userRoles == null || userRoles.Data == null || userRoles.Error == true) return new RespDto<bool>() { Error = true, Message = userRoles.Message };

                var validModifiedRoles = GetValidRoles(updatedRoles);

                var addedRoles = GetAddedRoles(validModifiedRoles, userRoles.Data);

                var removedRoles = GetRemovedRoles(validModifiedRoles, userRoles.Data);

                var isAdded = await _userManager.AddToRolesAsync(user, addedRoles);

                var isRemoved = await _userManager.RemoveFromRolesAsync(user, removedRoles);

                if (isAdded.Succeeded && isRemoved.Succeeded) { return new RespDto<bool>() { Data = true }; }
                return new RespDto<bool>() { Data = false, Error = true, Message = "An error Occured whilemodifing, please try again " };
            }
            catch (Exception ex)
            {

                return new RespDto<bool>() { Error = true, Message= "Please send a valid rules" };
            }
            
            
           
        }

        private List<string> GetValidRoles(IList<string> modifiedRoles)
        {
            var validRoles = new List<string>();
            var appRoles = AppRoles.GetAppRoles();
            foreach ( var role in modifiedRoles)
            {
                if(appRoles.Contains(role)) validRoles.Add(role);
            }
            return validRoles;
        }

        private List<string> GetAddedRoles(List<string> modifiedRoles, IList<string> userRoles)
        {
            var addedRoles = new List<string>();
            foreach (var role in modifiedRoles)
            {
                if (!userRoles.Contains(role)) addedRoles.Add(role);
            }
            return addedRoles;
        }

        private List<string> GetRemovedRoles(List<string> modifiedRoles, IList<string> userRoles)
        {
            var validUserRoles = this.GetValidRoles(userRoles);
            var removedRoles = new List<string>();
            foreach (var role in validUserRoles)
            {
                if (!modifiedRoles.Contains(role)) removedRoles.Add(role);
            }
            return removedRoles;
        }
    }
}
