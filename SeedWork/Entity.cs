namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork
{
    using System;
    using MediatR;
    using System.Collections.Generic;
    using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;

    public abstract class Entity<T> where T : ValueObject
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

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<T>))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity<T> item = (Entity<T>)obj;

            return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ 31;
        }
        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity<T> left, Entity<T> right)
        {
            return !(left == right);
        }
    }
}
