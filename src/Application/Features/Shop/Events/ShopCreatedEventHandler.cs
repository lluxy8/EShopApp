using AutoMapper;
using Core.Entities.Read;
using Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shop.Events
{
    public class ShopCreatedEventHandler : INotificationHandler<ShopCreatedEvent>
    {
        private readonly ReadDbContext _context;
        private readonly IMapper _mapper;

        public ShopCreatedEventHandler(
            ReadDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(ShopCreatedEvent notification, CancellationToken cancellation)
        {
            var shop = _mapper.Map<ShopView>(notification);
            await _context.AddAsync(shop, cancellation);
        }
    }
}
