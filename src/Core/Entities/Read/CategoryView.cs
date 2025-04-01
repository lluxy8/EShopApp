using Core.Common.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Read
{
    public class CategoryView : BaseEntity
    {
        public required string Name { get; set; }
    }
}
