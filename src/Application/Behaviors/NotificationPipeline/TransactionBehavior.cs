using Castle.Core.Logging;
using Core.Entities.Read;
using Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviors.NotificationPipeline
{
    public class TransactionBehavior
    {
    }

    public class TransactionBehavior<TNotification> : INotificationBehavior<TNotification>
        where TNotification : INotification
    {
        private readonly IReadDbUnitOfWork _uow;
        private readonly ILogger<TransactionBehavior> _logger;

        public TransactionBehavior(IReadDbUnitOfWork uow, ILogger<TransactionBehavior> logger)
        {
            _uow = uow;
            _logger = logger;
        }
        public async Task Handle(
            TNotification notification, 
            CancellationToken cancellationToken, 
            Func<Task> next)
        {
            try
            {
                await _uow.BeginTransactionAsync(cancellationToken);

                await next();

                await _uow.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Product creation failed in read DB: " + ex);
                await _uow.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
