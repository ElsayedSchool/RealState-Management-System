using Application.Authentication;
using Application.Authentication.Commands.DeactivateUserCommand;
using Application.Authentication.Commands.RemoveUserCommand;
using Application.Authentication.Commands.ResetPasswordCommand;
using Application.Authentication.Queries.GetUsersListQuery;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcademyProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IMediator Mediator;

        public AuthController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInCommand user)
        {
            return Ok(await Mediator.Send(user));
        }

        [HttpGet("signout")]
        public async Task<IActionResult> SignOut()
        {
            return Ok(await Mediator.Send(new SignOutCommand()));
        }


        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand newUserPassword)
        {
            return Ok(await Mediator.Send(newUserPassword));
        }

    }
}
