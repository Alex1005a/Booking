namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork
{
    using System;
    using MediatR;
    using System.Collections.Generic;

    public abstract class Entity<T> where T : IComparable, IEquatable<T>
    {
        T _Id;        
        public virtual T Id 
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
