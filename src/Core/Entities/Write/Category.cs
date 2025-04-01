using Core.Common.BaseClasses;

namespace Core.Entities.Write
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<Product> Products { get; set; } = [];
    }
}
