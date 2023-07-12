using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinEcomDomain.Enum
{
    [Flags]
    public enum Role
    {
        Administrator = 1,
        Staff = 2,
        Shipper = 4,
        Customer = 8
    }
}
