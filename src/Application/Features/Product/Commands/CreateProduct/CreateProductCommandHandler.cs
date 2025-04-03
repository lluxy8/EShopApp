using Application.Features.Product.Events.CreateProduct;
using AutoMapper;
using Core.Common.Results;
using Core.Entities.Read;
using Core.Entities.Write;
using Core.Interfaces;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler
            : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        private readonly WriteDbUnitOFWork _writeUow;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(
            WriteDbUnitOFWork writeUow,
            IMapper mapper,
            ILogger<CreateProductCommandHandler> logger,
            IMediator mediator)
        {
            _writeUow = writeUow;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellation)
        {
            ArgumentNullException.ThrowIfNull(request);

            var category = await _writeUow.WriteRepository<Category>()
                .GetByConditionAsync(c => c.Id == request.CategoryId, cancellation);

            var shop = await _writeUow.WriteRepository<Core.Entities.Write.Shop>()
                .GetByConditionAsync(s => s.Id == request.ShopId, cancellation);

            if (category == null || shop == null)
                return Result<Guid>.Failure("Category or Shop not found");

            var product = _mapper.Map<Core.Entities.Write.Product>(request);
            product.Id = Guid.NewGuid();

            await _writeUow.WriteRepository<Core.Entities.Write.Product>()
                .AddAsync(product, cancellation);

            var @event = new ProductCreatedEvent(
                product.Id,
                product.CategoryId,
                category.Name, 
                product.ShopId,
                shop.Name,    
                product.Name,
                product.Description,
                product.Stock,
                product.Price,
                product.CreatedDate,
                product.UpdatedDate
            );

            await _mediator.Publish(@event, cancellation);

            _logger.LogInformation("Product Created with ID: " + product.Id);

            return Result<Guid>.Success(product.Id);
        }
    }
}
