using Core.Common.BaseClasses;

namespace Core.Entities.Write
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Country { get; set; }
        public int Gender { get; set; }
        public ICollection<Address> Addresses { get; set; } = [];
        public Shop? Shop { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
