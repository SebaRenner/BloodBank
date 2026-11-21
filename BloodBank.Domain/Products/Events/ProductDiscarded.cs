using BloodBank.Domain.Common;

namespace BloodBank.Domain.Products.Events;

public record ProductDiscarded(Guid ProductId) : DomainEvent(ProductId)
{
}
