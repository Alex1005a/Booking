using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelSevice.Application.UseCases.Commands.UpdateHotelOwner
{
    public class UpdateHotelOwnerHandler : IRequestHandler<UpdateHotelOwner>
    {
        public IHotelRepository _hotelRepository;
        private readonly ISearchPort _searchPort;

        public UpdateHotelOwnerHandler(IHotelRepository hotelRepository, ISearchPort searchPort)
        {
            _hotelRepository = hotelRepository;
            _searchPort = searchPort;
        }

        public async Task<Unit> Handle(UpdateHotelOwner request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetAsync(new HotelId(request.HotelId));
            HotelOwner hotelOwner = new HotelOwner(request.Id, request.Name, request.PhoneNumber);

            hotel.TryChangeOwner(hotelOwner);
            _hotelRepository.Update(hotel);

            await _hotelRepository.UnitOfWork.SaveChangesAsync();

            await _searchPort.IndexAsync(hotel);

            return Unit.Value;
        }
    }
}
