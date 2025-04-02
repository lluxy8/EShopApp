using Core.Common.Results;
using MediatR;

namespace Application.Features.Product.Commands.CreateProduct
{
    public record CreateProductCommand(
        Guid CategoryId, 
        Guid ShopId,
        string Name,
        string Description,
        decimal Price,
        int Stock) : IRequest<Result<Guid>>;
}
