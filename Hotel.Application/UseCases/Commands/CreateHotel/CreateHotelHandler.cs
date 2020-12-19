using HotelSevice.Application.DomainEvents;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelSevice.Application.UseCases.Commands.CreateHotel
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
            Hotel hotelAggregate = new Hotel(
                HotelId.Generate(),
                request.Name, 
                request.Description, 
                new PhoneNumber(request.PhoneNumber), 
                DateTime.Now,
                request.Address,
                request.HotelOwner);

            var hotel = _hotelRepository.Add(hotelAggregate);
            await _hotelRepository.UnitOfWork.SaveChangesAsync();

            await _searchPort.IndexAsync(hotel);

            await _mediator.Publish(new CreateHotelEvent
            {
                Hotel = hotel
            });

            return hotel.Id.Value;
        }
    }
}
