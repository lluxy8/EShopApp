using Core.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Read
{
    public class AddressView : BaseEntity
    {
        public required string UserFullName { get; set; }
        public required string AdressType { get; set; }
        public string AdressLine1 { get; set; } = string.Empty;
        public string AdressLine2 { get; set; } = string.Empty;
        public required string City { get; set; }
        public required string Street { get; set; }
        public required string ZipCode { get; set; }
    }
}
