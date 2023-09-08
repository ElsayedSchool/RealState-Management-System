using AcademyProject.Controllers;
using Application.CommentApp.Command.DeleteComment;
using Application.CommentApp.Command.UpsertComment;
using Application.CommentApp.Query.GetAllOfferCommentsQuery;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace realState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : BaseController
    {
        // GET api/<CommentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAllOfferComments(int id)
        {
            return Ok(await Mediator.Send(new GetAllOfferCommentsQuery() { OfferId = id}));
        }

        // POST api/<CommentsController>
        [HttpPost]
        public async Task<ActionResult> UpsertComment([FromBody] UpsertCommentCommand value)
        {
            return Ok(await Mediator.Send(value));
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCommentCommand(){ Id =id }));
        }
    }
}
