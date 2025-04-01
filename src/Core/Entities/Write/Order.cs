using Core.Common.BaseClasses;

namespace Core.Entities.Write
{
    public class Order : BaseEntity
    {
        public Guid ShopId { get; set; }
        public required Shop Shop { get; set; }
        public Guid UserId { get; set; }
        public required User User { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; } = [];
        public required Address Address { get; set; }
        public Guid AddressId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Status { get; set; }
    }


    public class OrderProduct
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
