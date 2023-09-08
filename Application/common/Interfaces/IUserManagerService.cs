using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserManagerService
    {
        Task<RespDto<bool>> CreateUserAsync(string userName, string password, string Name);
        Task<IList<ApplicationUser>> GetAllUsersAsync();
        Task<bool> IsLockedOut(ApplicationUser user);
        Task<ApplicationUser?> GetUserById(string Id);
        Task<ApplicationUser?> GetUserByNameAsync(string username);
        Task<RespDto<bool>> DeleteUserAsync(string userId);
        Task<RespDto<bool>> ChangeUserPassword(ApplicationUser signInUser, string oldPassword, string newPassword);
        Task<RespDto<bool>> isPasswordValid(ApplicationUser signInUser, string password);
        Task<RespDto<bool>> DeactivateUser(string userId, bool Deactivate);
        Task<RespDto<bool>> ResetPassword(string userId);
        Task<RespDto<bool>> HasAny();
    }
}
