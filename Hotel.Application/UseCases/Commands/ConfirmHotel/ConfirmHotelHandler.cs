using HotelSevice.Application.DomainEvents;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HotelSevice.Application.UseCases.Commands.ConfirmHotel
{
    public class ConfirmHotelHandler : IRequestHandler<ConfirmHotel>
    {
        public IHotelRepository _hotelRepository;
        public IMediator _mediator;
        private readonly ISearchPort _searchPort;

        public ConfirmHotelHandler(IHotelRepository hotelRepository, IMediator mediator, ISearchPort searchPort)
        {
            _hotelRepository = hotelRepository;
            _mediator = mediator;
            _searchPort = searchPort;
        }

        public async Task<Unit> Handle(ConfirmHotel request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetAsync(request.HotelId);

            hotel.Confirm();
            hotel.AddDomainEvent(new ConfirmHotelEvent(hotel));
            await _hotelRepository.UnitOfWork.SaveChangesAsync();

            await _searchPort.IndexAsync(hotel);

            await _mediator.FixDomainEventsAsync(hotel);

            return Unit.Value;
        }
    }
}
