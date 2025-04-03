using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shop.Events
{
    public record ShopCreatedEvent(
        Guid Id,
        Guid UserId,
        string UserFullName,
        string Name,
        string Description,
        DateTime CreatedDate,
        DateTime? UpdatedDate) : INotification;
}
