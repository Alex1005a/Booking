using Hotel.Application.UseCases.Commands.UpdateHotelStatus;
using Hotel.Domain.AggregatesModel.HotelAggregate;
using Hotel.Infrastructure;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hotel.UnitTesting.Application
{
    public class UpdateHotelStatusTests
    {
        [Fact]
        public async Task Update_hotel_status_test()
        {
            Guid ownerId = Guid.NewGuid();
            bool approved = true;
            var mediator = new Mock<IMediator>();
            IHotelRepository repository = new FakeRepository();

            var cltToken = new System.Threading.CancellationToken();

            HotelAggregate hotel = new HotelAggregate(
                "Hotel",
                "desc",
                "+020 111 94546 333",
                new Address(1, "street", "city", "state", "country"),
                new HotelOwner(ownerId, "name", "+020 111 94546 333")
                );

            var hotel1 = repository.Add(hotel);

            //Act
            UpdateHotelStatus update = new UpdateHotelStatus(hotel1.Id, approved);
            UpdateHotelStatusHandler handler = new UpdateHotelStatusHandler(repository, mediator.Object);

            await handler.Handle(update, cltToken);

            var result = await repository.GetAsync(hotel1.Id);
            //Assert
            Assert.True(result.Approved == approved);
        }
    }
}
