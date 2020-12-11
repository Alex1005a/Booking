using HotelSevice.Application;
using HotelSevice.Application.UseCases.Commands.ConfirmHotel;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using HotelSevice.Infrastructure;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelSevice.UnitTesting.Application
{
    public class ConfirmHotelTests
    {
        [Fact]
        public async Task Update_hotel_status_test()
        {
            Guid ownerId = Guid.NewGuid();
            var mediator = new Mock<IMediator>();
            IHotelRepository repository = new FakeRepository();

            var cltToken = new System.Threading.CancellationToken();

            Hotel hotel = new Hotel(
                "Hotel",
                "desc",
                new PhoneNumber("+020 111 94546 333"),
                new Address(1, "street", "city", "state", "country"),
                new HotelOwner(ownerId, "name", "+020 111 94546 333")
                );

            var hotel1 = repository.Add(hotel);

            var searchMock = new Mock<ISearchPort>();

            //Act
            ConfirmHotel update = new ConfirmHotel(hotel1.Id);
            ConfirmHotelHandler handler = new ConfirmHotelHandler(repository, mediator.Object, searchMock.Object);

            await handler.Handle(update, cltToken);

            var result = await repository.GetAsync(hotel1.Id);
            //Assert
            Assert.True(result.Confirmed == true);
        }
    }
}
