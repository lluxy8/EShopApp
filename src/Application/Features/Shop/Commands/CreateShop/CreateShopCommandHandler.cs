using Application.Features.Shop.Events;
using AutoMapper;
using Castle.Core.Logging;
using Core.Common.Results;
using Core.Entities.Write;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shop.Commands.CreateShop
{
    public class CreateShopCommandHandler 
        : IRequestHandler<CreateShopCommand, Result<Guid>>
    {
        private readonly IWriteDbUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateShopCommandHandler> _logger;


        public CreateShopCommandHandler(
            IWriteDbUnitOfWork uow,
            IMapper mapper,
            IMediator mediator,
            ILogger<CreateShopCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        async Task<Result<Guid>> IRequestHandler<CreateShopCommand, Result<Guid>>.Handle(CreateShopCommand request, CancellationToken cancellation)
        {
            var user = await _uow.WriteRepository<User>().GetByConditionAsync(
                x => x.Id == request.UserId, cancellation);

            if (user is null)
                return Result<Guid>.Failure("Can't Find the user");

            var shop = _mapper.Map<Core.Entities.Write.Shop>(request);
            shop.Id = Guid.NewGuid();
            shop.User = user;

            var @event = new ShopCreatedEvent
            (
                shop.Id,
                user.Id,
                $"{shop.User.Name} {shop.User.Surname}",
                shop.Name,
                shop.Description,
                shop.CreatedDate,
                shop.UpdatedDate

            );

            await _mediator.Publish(@event, cancellation);

            _logger.LogInformation("Shop created with ID:" + shop.Id);

            return Result<Guid>.Success(shop.Id);
        }
    }
}
