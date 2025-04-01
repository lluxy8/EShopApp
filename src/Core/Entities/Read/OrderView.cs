using Core.Common.BaseClasses;

namespace Core.Entities.Read
{
    public class OrderView
    {
        public Guid ShopId { get; set; }
        public required string ShopName { get; set; }
        public Guid UserId { get; set; }
        public required string UserFullName { get; set; }
        public ICollection<OrderProductView> Products { get; set; } = [];
        public Guid AddressId { get; set; }
        public string Description { get; set; } = string.Empty;
        public required string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderProductView : BaseEntity
    {
        public Guid OrderId { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
