using AcademyProject.Controllers;
using Application.CommentApp.Command.UpsertComment;
using Application.FavoriteApp.Commands.IsFavorite;
using Application.FavoriteApp.Commands.UpsertFavorite;
using Application.FavoriteApp.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace realState.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : BaseController
    {

        // POST api/<FavoritesController>
        [HttpPost]
        public async Task<ActionResult> GetAllFavorites([FromBody] GetAllFavoritesQuery value)
        {
            return Ok(await Mediator.Send(value));
        }

        // POST api/<FavoritesController>
        [HttpPut]
        public async Task<ActionResult> UpsertFavoirte([FromBody] UpsertFavoriteCommand value)
        {
            return Ok(await Mediator.Send(value));
        }

        [HttpPut("isfavorite")]
        public async Task<ActionResult> IsFavorite([FromBody] IsFavoriteCommand value)
        {
            return Ok(await Mediator.Send(value));
        }


    }
}
