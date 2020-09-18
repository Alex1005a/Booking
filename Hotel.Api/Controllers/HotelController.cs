using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelSevice.Application.UseCases.Queries.GetHotelById;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public Hotel Get([FromQuery] string id)
        {
            return _mediator.Send(new GetHotelById(id)).Result;
        }
    }
}
