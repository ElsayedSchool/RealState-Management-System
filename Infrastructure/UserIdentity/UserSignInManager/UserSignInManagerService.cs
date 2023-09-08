using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UserIdentity.UserSignInManager
{
    public class UserSignInManagerService : IUserSignInManagerService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserSignInManagerService(SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<bool> IsValidSignInUserAsync(ApplicationUser user)
        {
            return await _signInManager.CanSignInAsync(user);
        }

        public async Task<bool> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public async Task AddRoleAsync(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);
        }
    }
}
