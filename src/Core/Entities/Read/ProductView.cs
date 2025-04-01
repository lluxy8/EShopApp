using Core.Common.BaseClasses;

namespace Core.Entities.Read
{
    public class ProductView : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public Guid ShopId { get; set; }
        public required string ShopName { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
