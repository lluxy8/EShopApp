using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface INotificationBehavior<TNotification>
        where TNotification : INotification
    {
        Task Handle(TNotification notification,
                   CancellationToken cancellationToken,
                   Func<Task> next);
    }
}
