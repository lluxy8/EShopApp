using AutoMapper;
using Castle.Core.Logging;
using Core.Entities.Read;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Product.Events.CreateProduct
{
    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly ReadDbUnitOfWork _uow;
        private readonly ReadDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductCreatedEventHandler> _logger;

        public ProductCreatedEventHandler(
            IMapper mapper,
            ReadDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<ProductView>(notification);
            await _context.AddAsync(product, cancellationToken);
        }
    }
}
