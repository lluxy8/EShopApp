using Core.Common.BaseClasses;
using Core.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Write
{
    public class Admin : BaseEntity
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public int Role { get; set; }
    }
}
