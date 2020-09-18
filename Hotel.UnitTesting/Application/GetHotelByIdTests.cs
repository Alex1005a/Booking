using HotelSevice.Application;
using HotelSevice.Application.Pipelines;
using HotelSevice.Application.UseCases.Queries.GetHotelById;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HotelSevice.UnitTesting.Application
{
    public class GetHotelByIdTests
    {
        [Fact]
        public async Task Handle_not_return_null()
        {           
            string id = Guid.NewGuid().ToString();
            var fakeCmd = new GetHotelById(id);
            Hotel hotel = new Hotel(
                "Hotel",
                "desc",
                "+020 111 94546 333",
                new Address(1, "street", "city", "state", "country"),
                new HotelOwner(Guid.NewGuid(), "name", "+020 111 94546 333")
                );
            var searchMock = new Mock<ISearchPort>();
            searchMock.Setup(svc => svc.GetByIdAsync(id)).Returns(Task.FromResult(hotel));
            //Act
            var handler = new GetHotelByIdHandler(searchMock.Object);
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

            Hotel hotel = new Hotel(
                "Hotel",
                "desc",
                "+020 111 94546 333",
                new Address(1, "street", "city", "state", "country"),
                new HotelOwner(Guid.NewGuid(), "name", "+020 111 94546 333")
                );


            var searchMock = new Mock<ISearchPort>();
            searchMock.Setup(svc => svc.GetByIdAsync(id)).Returns(Task.FromResult(hotel));

            var opts = Options.Create(new MemoryDistributedCacheOptions());
            IDistributedCache cache = new MemoryDistributedCache(opts);

            var handler = new GetHotelByIdHandler(searchMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            IPipelineBehavior<GetHotelById, Hotel> pipeline = new CacheBehavior<GetHotelById, Hotel>(cache);
            //Act

            var del = new RequestHandlerDelegate<Hotel>(() => handler.Handle(fakeCmd, cltToken));
            var res = await pipeline.Handle(fakeCmd, cltToken, del);
            var cacheString = await cache.GetStringAsync(fakeCmd.CacheKey);
            var cacheValue = JsonConvert.DeserializeObject<Hotel>(cacheString);
            //Assert
            Assert.True(res.Name == cacheValue.Name);
        }
    }
}
