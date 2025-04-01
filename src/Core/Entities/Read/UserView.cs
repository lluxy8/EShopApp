using Core.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Read
{
    public class UserView : BaseEntity
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Country { get; set; }
        public required string Gender { get; set; }
        public string? Shop { get; set; }
        public ICollection<AddressView> Addresses { get; set; } = [];
        public DateTime DateOfBirth { get; set; }
        public int Age => DateTime.UtcNow.Year - DateOfBirth.Year;
    }
}
