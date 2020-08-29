using Hotel.Application.DomainEvents;
using Hotel.Domain.AggregatesModel.HotelAggregate;
using Hotel.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Application.UseCases.Commands.UpdateHotelStatus
{
    public class UpdateHotelStatusHandler : IRequestHandler<UpdateHotelStatus>
    {
        public IHotelRepository _hotelRepository;
        public IMediator _mediator;

        public UpdateHotelStatusHandler(IHotelRepository hotelRepository, IMediator mediator)
        {
            _hotelRepository = hotelRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateHotelStatus request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetAsync(request.HotelId);

            hotel.SetStatus(request.Status);
            hotel.AddDomainEvent(new UpdateHotelStatusEvent(hotel, request.Status));
            await _hotelRepository.UnitOfWork.SaveChangesAsync();

            await _mediator.FixDomainEventsAsync(hotel);

            return Unit.Value;
        }
    }
}
