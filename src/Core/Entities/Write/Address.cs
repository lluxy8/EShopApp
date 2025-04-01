using Core.Common.BaseClasses;

namespace Core.Entities.Write
{
    public class Address : BaseEntity
    {
        public Guid UserId { get; set; }
        public required User User { get; set; }
        public required string Title { get; set; }
        public int AddressType { get; set; }
        public string AdressLine1 { get; set; } = string.Empty;
        public string AdressLine2 { get; set; } = string.Empty;
        public required string City { get; set; }
        public required string Street { get; set; }
        public required string ZipCode { get; set; }
    }
}
