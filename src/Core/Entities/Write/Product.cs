using Core.Common.BaseClasses;

namespace Core.Entities.Write
{
    public class Product : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public required Category Category { get; set; }
        public Guid ShopId { get; set; }
        public required Shop Shop { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
