using HotelSevice.Application;
using HotelSevice.Application.Pipelines;
using HotelSevice.Application.UseCases.Commands.UpdateHotelOwner;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using HotelSevice.Infrastructure;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelSevice.UnitTesting.Application
{
    public class UpdateHotelOwnerTests 
    {
        [Fact]
        public async Task Update_Owner_test()
        {
            Guid ownerId = Guid.NewGuid();
            string newName = "eman";

            IHotelRepository repository = new FakeRepository();
            var Mock = new Mock<ISearchPort>();

            var cltToken = new System.Threading.CancellationToken();

            Hotel hotel = new Hotel(
                HotelId.Generate(),
                "Hotel",
                "desc",
                new PhoneNumber("+020 111 94546 333"),
                new Address(1, "street", "city", "state", "country"),
                new HotelOwner(ownerId, "name", "+020 111 94546 333")
                );

            var hotel1 = repository.Add(hotel);

            //Act
            UpdateHotelOwner update = new UpdateHotelOwner(hotel1.Id.Value, ownerId, "eman", "+020 111 94546 333");
            UpdateHotelOwnerHandler handler = new UpdateHotelOwnerHandler(repository, Mock.Object);
            
            await handler.Handle(update, cltToken);

            var result = await repository.GetAsync(hotel1.Id);
            //Assert
            Assert.True(newName == result.HotelOwner.Name);
        }

        [Fact]
        public async Task Test_cache_remove()
        {
            string hotelId = Guid.Empty.ToString();

            var opts = Options.Create(new MemoryDistributedCacheOptions());
            IDistributedCache cache = new MemoryDistributedCache(opts);

            IRequestPostProcessor<UpdateHotelOwner, Unit> removeCacheBehavior = new RemoveCacheBehavior<UpdateHotelOwner, Unit>(cache);

            UpdateHotelOwner request = new UpdateHotelOwner(hotelId, Guid.Empty, "eman", "+020 111 94546 333");

            var cltToken = new System.Threading.CancellationToken();

            //Act
            await cache.SetStringAsync(request.CacheKeys[0], "value");

            await removeCacheBehavior.Process(request, Unit.Value, cltToken);
            //Assert
            Assert.Null(cache.Get(request.CacheKeys[0]));
        }
    }
}
