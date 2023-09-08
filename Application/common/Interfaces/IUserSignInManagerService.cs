using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IUserSignInManagerService
    {
        Task<bool> IsValidSignInUserAsync(ApplicationUser user);
        Task<bool> SignOutAsync();
        Task AddRoleAsync(IdentityRole role);
    }
}