using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Security
{

    public enum PermissionTypes
    {
        Retrieve = 1,
        Create = 2,
        Update = 4,
        Delete = 8,
        All=16
    }

}
