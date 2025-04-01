using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Enums.Admin
{
    [Flags]
    public enum AdminPermission
    {
        None = 0,
        Read = 1,
        Write = 2,
        Delete = 4,
        All = Read | Write | Delete // 3
    }
}
