using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;

namespace HotelSevice.Application.DomainEvents
{
    public class ConfirmHotelEvent : INotification
    {
        public HotelId HotelId { get; set; }

        public ConfirmHotelEvent(HotelId hotelId)
        {
            HotelId = hotelId;
        }
    }
}
