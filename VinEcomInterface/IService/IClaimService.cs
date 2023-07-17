using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;

namespace VinEcomInterface.IService
{
    public interface IClaimService
    {
        int GetCurrentUserId();
        int GetStoreId();
        int GetRoleId();
        Role? GetRole();
    }
}
