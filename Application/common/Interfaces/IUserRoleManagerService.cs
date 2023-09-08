using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.common.Interfaces
{
    public interface IUserRoleManagerService
    {
        Task<RespDto<IList<string>>> GetUserRolesAsync(string userId);
        Task<RespDto<bool>> AddUserToRoleAsync(ApplicationUser user, string roleName);
        Task<RespDto<bool>> ChangeUserRolesAsync(string userId, List<string> userRoles);
    }
}
