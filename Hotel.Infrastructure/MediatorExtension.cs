using MediatR;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure
{
    static class MediatorExtension
    {
        public static async Task FixDomainEventsAsync(this IMediator mediator, List<Entity> domainEntities)
        {
            var domainEvents = domainEntities
                .SelectMany(x => x.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
