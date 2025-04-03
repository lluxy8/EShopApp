using Core.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shop.Commands.CreateShop
{
    public record CreateShopCommand(
        Guid UserId,
        string UserFullName,
        string Name,
        string Description) : IRequest<Result<Guid>>;
}
