using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Enums.Order
{
    public enum OrderStatus
    {
        Unknown = 0,
        Pending = 1,
        Processing = 2,
        Completed = 3,
        Cancelled = 4,
        Refunded = 5,
        Failed = 6
    }
}
