using Hotel.Application.UseCases.Commands.UpdateHotelOwner;
using Hotel.Domain.AggregatesModel.HotelAggregate;
using Hotel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hotel.UnitTesting.Application
{
    public class UpdateHotelOwnerTests 
    {
        [Fact]
        public async Task Update_Owner_test()
        {
            Guid ownerId = Guid.NewGuid();
            string newName = "eman";

            IHotelRepository repository = new FakeRepository();

            var cltToken = new System.Threading.CancellationToken();

            HotelAggregate hotel = new HotelAggregate(
                "Hotel",
                "desc",
                "+020 111 94546 333",
                new Address(1, "street", "city", "state", "country"),
                new HotelOwner(ownerId, "name", "+020 111 94546 333")
                );

            string hotelId = repository.Add(hotel);

            //Act
            UpdateHotelOwner update = new UpdateHotelOwner(hotelId, ownerId, "eman", "+020 111 94546 333");
            UpdateHotelOwnerHandler handler = new UpdateHotelOwnerHandler(repository);
            
            await handler.Handle(update, cltToken);

            var result = await repository.GetAsync(hotelId);
            //Assert
            Assert.True(newName == result.HotelOwner.Name);
        }
    }
}
