using Application.Features.Product.Events.CreateProduct;
using AutoMapper;
using Core.Common.Results;
using Core.Entities.Read;
using Core.Entities.Write;
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
            var category = await _writeUow.WriteRepository<Category>()
                .GetByConditionAsync(x => x.Id == request.CategoryId, cancellation);

            var shop = await _writeUow.WriteRepository<Shop>()
                .GetByConditionAsync(x => x.Id == request.ShopId, cancellation);

            if (category == null || shop == null)
                return Result<Guid>.Failure("Category or Shop not found.");

            var id = Guid.NewGuid();
            var product = _mapper.Map<Core.Entities.Write.Product>(request);
            product.Id = id;

            await _writeUow.WriteRepository<Core.Entities.Write.Product>()
                .AddAsync(product, cancellation);

            var @event = _mapper.Map<ProductCreatedEvent>(product) 
                with
            {
                CategoryName = product.Category.Name,
                ShopName = product.Shop.Name
            };

            await _mediator.Publish(@event, cancellation);

            _logger.LogInformation("Product created: " + id);

            return Result<Guid>.Success(id);
        }
    }
}
