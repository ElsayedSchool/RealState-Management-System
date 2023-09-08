using AcademyProject.Controllers;
using Application.Common.Interfaces;
using Application.OfferApp.Commands.ChangeDepartment;
using Application.OfferApp.Commands.DeleteOffer;
using Application.OfferApp.Commands.DeleteOffers;
using Application.OfferApp.Commands.UpsertOffer;
using Application.OfferApp.Queries.GetOfferDetail;
using Application.OfferApp.Queries.GetOffersList;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace realState.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : BaseController
    {
        private readonly ICurrentUserService _currentUser;

        public OfferController(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }
        // GET api/<OfferController>/5
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetOfferDetail(int Id)
        {
            return Ok(await Mediator.Send(new GetOfferDetailQuery() { OfferId = Id}));
        }


        // GET: api/<OfferController>
        [HttpPost]
        public async Task<ActionResult>  GetAllOffersByQuery(GetAllOffersQuery query)
        {
            return Ok(await Mediator.Send(query));
        }


        // POST api/<OfferController>
        [HttpPut]
        public async Task<ActionResult> AddOffer([FromForm] UpsertOfferCommand offer)
        {
            return Ok(await Mediator.Send(offer));
        }

        [Authorize(Roles = "Admin")]
        // POST api/<OfferController>
        [HttpPut("changedepartment")]
        public async Task<ActionResult> ChangeOffersDepartment([FromBody] ChangeDepartmentCommand offers)
        {
            
            return Ok(await Mediator.Send(offers));
        }


        // DELETE api/<OfferController>/5
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteOffer(int Id)
        {
            return Ok(await Mediator.Send(new DeleteOfferCommand() { Id = Id }));
        }

        [Authorize(Roles = "Admin")]
        // DELETE api/<OfferController>/5
        [HttpPost("deleteoffers")]
        public async Task<ActionResult> DeleteOffers(DeleteOffersCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
