using Core.Entities.Read;
using MediatR;

namespace Application.Features.Product.Events.CreateProduct
{
    public sealed record ProductCreatedEvent(
        Guid Id,
        Guid CategoryId,
        string CategoryName,
        Guid ShopId,
        string ShopName,
        string Name,
        string Description,
        int Stock,
        decimal Price,
        DateTime CreatedDate,
        DateTime? UpdatedDate) : INotification;
}
