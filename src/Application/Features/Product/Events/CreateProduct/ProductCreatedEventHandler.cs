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
            ReadDbUnitOfWork uow, 
            IMapper mapper,
            ReadDbContext context,
            ILogger<ProductCreatedEventHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }
        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await _uow.BeginTransactionAsync(cancellationToken);

                var product = _mapper.Map<ProductView>(notification);
                await _context.AddAsync(product, cancellationToken);

                await _uow.CommitAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogError("Product creation failed in read DB:" + ex);
                await _uow.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
