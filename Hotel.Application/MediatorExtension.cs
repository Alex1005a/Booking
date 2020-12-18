using MediatR;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSevice.Application
{
    static class MediatorExtension
    {
        public static async Task FixDomainEventsAsync<T>(this IMediator mediator, Entity<T> domainEntity)
            where T : ValueObject
        {
            var domainEvents = domainEntity.DomainEvents;

            domainEntity.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
