using Hotel.Application.Pipelines;
using Hotel.Application.UseCases.Commands.UpdateHotelOwner;
using Hotel.Domain.AggregatesModel.HotelAggregate;
using Hotel.Infrastructure;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
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
            await cache.SetStringAsync(request.CacheKey, "value");

            await removeCacheBehavior.Process(request, Unit.Value, cltToken);
            //Assert
            Assert.Null(cache.Get(request.CacheKey));
        }
    }
}
