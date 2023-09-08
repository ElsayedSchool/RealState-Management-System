using Application.CategoryApp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcademyProject.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {


        // GET: api/<CategoryController>
        [HttpGet("all")]
        public async Task<ActionResult> GetAllCategoryes()
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UpsertCategoryCommand category)
        {
        
            return Ok(await Mediator.Send(category));
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            return Ok(await Mediator.Send(new DeleteCategoryCommand() { Id = Id}));
        }
    }
}
