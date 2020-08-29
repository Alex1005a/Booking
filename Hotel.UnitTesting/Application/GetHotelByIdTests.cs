using Hotel.Application.Pipelines;
using Hotel.Application.UseCases.Queries.GetHotelById;
using Hotel.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Hotel.UnitTesting.Application
{
    public class GetHotelByIdTests
    {
        [Fact]
        public async Task Handle_not_return_null()
        {           
            string id = Guid.NewGuid().ToString();
            var fakeCmd = new GetHotelById(id);
            HotelAggregate hotel = new HotelAggregate(
                "Hotel",
                "desc",
                "+020 111 94546 333",
                new Address(1, "street", "city", "state", "country"),
                new HotelOwner(Guid.NewGuid(), "name", "+020 111 94546 333")
                );
            var repoMock = new Mock<IHotelRepository>();
            repoMock.Setup(svc => svc.GetAsync(id)).Returns(Task.FromResult(hotel));
            //Act
            var handler = new GetHotelByIdHandler(repoMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeCmd, cltToken);

            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public async Task Handle_result_equal_cache()
        {
            string id = Guid.NewGuid().ToString();
            var fakeCmd = new GetHotelById(id);

            HotelAggregate hotel = new HotelAggregate(
                "Hotel",
                "desc",
                "+020 111 94546 333",
                new Address(1, "street", "city", "state", "country"),
                new HotelOwner(Guid.NewGuid(), "name", "+020 111 94546 333")
                );


            var repoMock = new Mock<IHotelRepository>();
            repoMock.Setup(svc => svc.GetAsync(id)).Returns(Task.FromResult(hotel));

            var opts = Options.Create(new MemoryDistributedCacheOptions());
            IDistributedCache cache = new MemoryDistributedCache(opts);

            var handler = new GetHotelByIdHandler(repoMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            IPipelineBehavior<GetHotelById, HotelAggregate> pipeline = new CacheBehavior<GetHotelById, HotelAggregate>(cache);
            //Act

            var del = new RequestHandlerDelegate<HotelAggregate>(() => handler.Handle(fakeCmd, cltToken));
            var res = await pipeline.Handle(fakeCmd, cltToken, del);
            var cacheString = await cache.GetStringAsync(fakeCmd.CacheKey);
            var cacheValue = JsonConvert.DeserializeObject<HotelAggregate>(cacheString);
            //Assert
            Assert.True(res.Name == cacheValue.Name);
        }
    }
}
