using Core.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Read
{
    public class AdminView : BaseEntity
    {
        public required string Username { get; set; }
        public required string Role { get; set; }
    }
}
