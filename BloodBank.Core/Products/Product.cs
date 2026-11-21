using BloodBank.Domain.Common;

namespace BloodBank.Domain.Products;

public class Product : AggregateRoot
{
    protected override void Apply(DomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            default: throw new InvalidOperationException($"Unhandled event {domainEvent.GetType().Name}");
        }
    }
}
