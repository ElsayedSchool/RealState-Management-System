using Application.common.Interfaces;
using Application.common.Interfaces.EntityRepositories;
using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, bool>
    {
        private readonly IUserManagerService _userManager;
        private readonly IUserRoleManagerService _roleManager;
        private readonly IApplicationRepo _repo;

        public SignUpCommandHandler(IUserManagerService userManager,IUserRoleManagerService roleManager, IApplicationRepo repo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _repo = repo;
        }

        public async Task<RespDto<bool>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var isCreated = await _userManager.CreateUserAsync(request.Email,request.Password, request.Name);
            if(isCreated == null || isCreated.Error == true)
            {
                return isCreated;
            }
            var user = await _userManager.GetUserByNameAsync(request.Email);
            var updatedUser = await _roleManager.AddUserToRoleAsync(user, AppRoles.User);
            var userData = await _repo.UserRepo.CreateAsync(new User() { Name = request.Name, Id = user.Id });
            if (userData == null || userData.Error == true) return userData;
            return updatedUser;
        }
    }
}
