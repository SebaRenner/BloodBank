using BloodBank.Domain.Common;

namespace BloodBank.Domain.Products.Events;

public record ProductCreated(Guid ProductId) : DomainEvent(ProductId)
{
}
