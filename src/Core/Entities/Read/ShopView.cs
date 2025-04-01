using Core.Common.BaseClasses;

namespace Core.Entities.Read
{
    public class ShopView : BaseEntity
    {
        public Guid UserId { get; set; }
        public required string UserFullName { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public virtual ICollection<OrderView> Orders { get; set; } = [];
        public virtual ICollection<ProductView> Products { get; set; } = [];
    }
}
