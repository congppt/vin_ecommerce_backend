using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Resources;

namespace VinEcomDomain.Enum
{
    public enum OrderStatus
    {
        Cart,
        Preparing,
        Shipping,
        Done,
        Cancel
    }
}
