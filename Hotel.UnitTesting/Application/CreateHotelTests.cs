using FluentValidation;
using HotelSevice.Application;
using HotelSevice.Application.DomainEvents;
using HotelSevice.Application.DomainEventsHandlers;
using HotelSevice.Application.Pipelines;
using HotelSevice.Application.UseCases.Commands.CreateHotel;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using HotelSevice.Infrastructure;
using MassTransit.Testing;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelSevice.UnitTesting.Application
{
    public class CreateHotelTests
    {
        [Fact]
        public Task Test_fluent_validator()
        {
            IPipelineBehavior<CreateHotel, string> pipeline = new ValidationPipe<CreateHotel, string>(new List<IValidator<CreateHotel>> { new CreateHotelValidator() });

            var fakeCmd = new CreateHotel { Name = "ww"};
            var cltToken = new System.Threading.CancellationToken();
            //Act
            var del = new RequestHandlerDelegate<string>(() => Task.FromResult(Guid.Empty.ToString()));

            Assert.ThrowsAsync<ValidationException>(async () => await pipeline.Handle(fakeCmd, cltToken, del));

            return Task.CompletedTask;
        }

        [Fact]
        public Task Test_send_message()
        {
            var harness = new InMemoryTestHarness();

            Hotel hotel = new Hotel(
               "Hotel",
               "desc",
               "+020 111 94546 333",
               new Address(1, "street", "city", "state", "country"),
               new HotelOwner(Guid.Empty, "name", "+020 111 94546 333")
               );


            var hotelEvent = new CreateHotelEvent
            {
                Hotel = hotel
            };

            harness.Start().Wait();
            var bus = harness.Bus;

            var cltToken = new System.Threading.CancellationToken();
            //Act
            
            CreateHotelEventHandler eventHandler = new CreateHotelEventHandler(bus);

            eventHandler.Handle(hotelEvent, cltToken).Wait();

            //Assert
            Assert.True(harness.Published.Select<CreateHotelEvent>().Any());

            return Task.CompletedTask;
        }
    }
}
