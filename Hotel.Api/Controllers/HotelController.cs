using System.Collections.Generic;
using System.Threading.Tasks;
using HotelSevice.Api.Infrastructure.Filters;
using HotelSevice.Application.UseCases.Commands.CreateHotel;
using HotelSevice.Application.UseCases.Queries.GetHotelById;
using HotelSevice.Application.UseCases.Queries.SearchHotelByName;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelSevice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Hotel> Get([FromQuery] string id)
        {
            return await _mediator.Send(new GetHotelById(id));
        }

        [HttpGet("searchByName")]
        public async Task<IEnumerable<Hotel>> Get([FromQuery] string name, [FromQuery] int page = 0)
        {
            return await _mediator.Send(new SearchHotelByName(name, page));
        }

        [HttpPost]
        [ServiceFilter(typeof(IdempotencyFilter))]
        public async Task<string> Post([FromBody] CreateHotel createHotel)
        {
            return await _mediator.Send(createHotel);
        }
    }
}
