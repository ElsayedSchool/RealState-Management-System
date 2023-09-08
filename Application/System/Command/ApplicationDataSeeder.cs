using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.System.Command
{
    public class ApplicationDataSeeder
    {
        private readonly IApplicationRepo _applicationRepo;
        private readonly IUserManagerService _userManager;
        private readonly IUserSignInManagerService _userSignInManager;
        private readonly IUserRoleManagerService _userRoleManager;
        private readonly IOptions<AppSettingModel> _options;

        public ApplicationDataSeeder(
            IApplicationRepo applicationRepo, 
            IUserManagerService userManager,
            IUserSignInManagerService userSignInManager,
            IUserRoleManagerService userRoleManager,
            IOptions<AppSettingModel> options)
        {
            _applicationRepo = applicationRepo;
            _userManager = userManager;
            _userSignInManager = userSignInManager;
            _userRoleManager = userRoleManager;
            _options = options;
        }

        public async  Task SeedAllAsync(CancellationToken cancellationToken)
        {
           var users = await _userManager.HasAny();
            if (users != null && users.Data != true)
            {
                await SeedRoles();
                await SeedAdmins();
            }
            await _applicationRepo.Commit();
           
        }

        private async Task SeedRoles()
        {
            var Roles = new List<IdentityRole>()
            {
                new IdentityRole() {Name = AppRoles.Admin},
                new IdentityRole() {Name = AppRoles.Updater},
                new IdentityRole() {Name = AppRoles.Remover},
                new IdentityRole() {Name = AppRoles.Uploader},
                new IdentityRole() {Name = AppRoles.User},
                new IdentityRole() {Name = AppRoles.Exporter},
            };
            foreach(var role in Roles)
            {
                await _userSignInManager.AddRoleAsync(role);
            }
        }

        private async Task SeedAdmins()
        {
            var admins = new List<ApplicationUser>()
            {
                new ApplicationUser() {UserName = "AppAdminA"},
                new ApplicationUser() {UserName = "AppAdminB"}
            };

            foreach (var admin in admins)
            {
                await _userManager.CreateUserAsync(admin.UserName, _options.Value.AdminPassword, admin.UserName);
                var user = await _userManager.GetUserByNameAsync(admin.UserName);
                await _userRoleManager.AddUserToRoleAsync(user, AppRoles.Admin );
                await _applicationRepo.UserRepo.CreateAsync(new User() { Id = user.Id, Name = user.UserName == "AppAdminA"? "المدير الاول" : "المدير الثانى" });
            };
        }
    }
}
