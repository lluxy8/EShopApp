using Core.Common.BaseClasses;

namespace Core.Entities.Write
{
    public class Shop : BaseEntity
    {
        public Guid UserId { get; set; }
        public required User User { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public ICollection<Order> Orders { get; set; } = [];
        public ICollection<Product> Products { get; set; } = [];
    }
}
