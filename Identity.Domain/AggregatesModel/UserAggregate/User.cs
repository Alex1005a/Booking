using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;

namespace Identity.Domain.AggregatesModel.UserAggregate
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        public Name Name { get; private set; }
    }
}
