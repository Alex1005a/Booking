using Hotel.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Application.UseCases.Commands.UpdateHotelOwner
{
    public class UpdateHotelOwnerHandler : IRequestHandler<UpdateHotelOwner>
    {
        public IHotelRepository _hotelRepository;

        public UpdateHotelOwnerHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<Unit> Handle(UpdateHotelOwner request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetAsync(request.HotelId);
            HotelOwner hotelOwner = new HotelOwner(request.Id, request.Name, request.PhoneNumber);

            hotel.UpdateOwner(hotelOwner);
            _hotelRepository.Update(hotel);

            await _hotelRepository.UnitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
