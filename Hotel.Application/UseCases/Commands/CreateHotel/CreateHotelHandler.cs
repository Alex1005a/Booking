using Hotel.Application.DomainEvents;
using Hotel.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Application.UseCases.Commands.CreateHotel
{
    public class CreateHotelHandler : IRequestHandler<CreateHotel, string>
    {
        public readonly IHotelRepository _hotelRepository;
        private readonly ISearchPort _searchPort;
        public readonly IMediator _mediator;

        public CreateHotelHandler(IHotelRepository hotelRepository, IMediator mediator, ISearchPort searchPort)
        {
            _hotelRepository = hotelRepository;
            _mediator = mediator;
            _searchPort = searchPort;
        }

        public async Task<string> Handle(CreateHotel request, CancellationToken cancellationToken)
        {
            HotelAggregate hotelAggregate = new HotelAggregate(
                request.Name, 
                request.Description, 
                request.PhoneNumber, 
                request.Address,
                request.HotelOwner);

            var hotel = _hotelRepository.Add(hotelAggregate);
            await _hotelRepository.UnitOfWork.SaveChangesAsync();

            _searchPort.Index(hotel);

            await _mediator.Publish(new CreateHotelEvent
            {
                Hotel = hotel
            });

            return hotel.Id;
        }
    }
}
