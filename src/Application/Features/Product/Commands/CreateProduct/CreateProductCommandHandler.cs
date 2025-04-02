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
        private readonly ReadDbUnitOfWork _readUow;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(
            WriteDbUnitOFWork writeUow,
            ReadDbUnitOfWork readUow,
            IMapper mapper,
            ILogger<CreateProductCommandHandler> logger,
            IMediator mediator)
        {
            _writeUow = writeUow;
            _readUow = readUow;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellation)
        {
            var shop = await _readUow.ReadRepository<Shop>()
                .GetByIdAsync(request.ShopId, cancellation);

            var category = await _readUow.ReadRepository<Category>()
                .GetByIdAsync(request.CategoryId, cancellation);

            if (category is null)
                return Result<Guid>.Failure("Category not found");
            else if (shop is null)
                return Result<Guid>.Failure("Shop not found");

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
