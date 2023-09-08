using AcademyProject.Controllers;
using Application.Authentication;
using Application.Authentication.Commands.DeactivateUserCommand;
using Application.Authentication.Commands.ResetPasswordCommand;
using Application.Authentication.Queries.GetUsersListQuery;
using Application.Authorization.Commands.ChangeRolesCommand;
using Application.Authorization.Queries.GetRolesQuery;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace realState.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : BaseController
    {
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpCommand newUser)
        {
            return Ok(await Mediator.Send(newUser));
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand resetPasswordCommand)
        {
            return Ok(await Mediator.Send(resetPasswordCommand));
        }

        [HttpPost("deactivateuser")]
        public async Task<IActionResult> DeactivateUser(DeactivateUserCommand deactivateUserCommand)
        {
            return Ok(await Mediator.Send(deactivateUserCommand));
        }

        /*[HttpPost("removeuser")]
        public async Task<IActionResult> RemoveUser(RemoveUserCommand removeUserCommand)
        {
            return Ok(await Mediator.Send(removeUserCommand));
        }*/
        [HttpGet]
        public async Task<IActionResult> GetAppUsers()
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery()));
        }

        // GET: api/<AuthorizationController>
        [HttpPost("userroles")]
        public async Task<ActionResult> GetUserRoles(GetUserRolesQuery userRolesQuery)
        {
            return  Ok(await Mediator.Send(userRolesQuery));
        }

        // POST api/<AuthorizationController>
        [HttpPost]
        public async Task<ActionResult> ChangeUserRole([FromBody] ChangeRolesCommand changeRolesCommand)
        {
            return Ok(await Mediator.Send(changeRolesCommand));
        }

        [HttpGet("approles")]
        public async Task<ActionResult> GetAppRoles()
        {
            return Ok(new List<string>() { AppRoles.Updater,AppRoles.Uploader,AppRoles.Exporter,AppRoles.Remover});
        }
    }
}
