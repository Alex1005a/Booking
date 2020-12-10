using HotelSevice.Application.DomainEvents;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HotelSevice.Application.UseCases.Commands.UpdateHotelStatus
{
    public class UpdateHotelStatusHandler : IRequestHandler<UpdateHotelStatus>
    {
        public IHotelRepository _hotelRepository;
        public IMediator _mediator;
        private readonly ISearchPort _searchPort;

        public UpdateHotelStatusHandler(IHotelRepository hotelRepository, IMediator mediator, ISearchPort searchPort)
        {
            _hotelRepository = hotelRepository;
            _mediator = mediator;
            _searchPort = searchPort;
        }

        public async Task<Unit> Handle(UpdateHotelStatus request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetAsync(request.HotelId);

            hotel.SetStatus(request.Status);
            hotel.AddDomainEvent(new UpdateHotelStatusEvent(hotel, request.Status));
            await _hotelRepository.UnitOfWork.SaveChangesAsync();

            await _searchPort.IndexAsync(hotel);

            await _mediator.FixDomainEventsAsync(hotel);

            return Unit.Value;
        }
    }
}
