using AcademyProject.Controllers;
using Application.SharedApp.Commands.AddSharedOffers;
using Application.SharedApp.Queries.GetSharedOffers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace realState.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SharesController : BaseController
    {
        // POST api/<SharessController>
        [HttpPost]
        public async Task<ActionResult> GetAllSharess([FromBody] GetAllSharedOffersQuery value)
        {
            return Ok(await Mediator.Send(value));
        }

        // POST api/<SharessController>
        [HttpPut]
        public async Task<ActionResult> UpsertComment([FromBody] AddSharedOffersCommand value)
        {
            return Ok(await Mediator.Send(value));
        }
    }
}
