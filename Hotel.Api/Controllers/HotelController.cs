using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Application.UseCases.Queries.GetHotelById;
using Hotel.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers
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
        public HotelAggregate Get([FromQuery] string id)
        {
            return _mediator.Send(new GetHotelById(id)).Result;
        }
    }
}
