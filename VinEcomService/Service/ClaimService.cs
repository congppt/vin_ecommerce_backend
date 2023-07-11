using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomInterface.IService;

namespace VinEcomService.Service
{
    public class ClaimService : IClaimService
    {
        IHttpContextAccessor contextAccessor;
        public ClaimService(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        public int GetCurrentUserId()
        {
            var id = contextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
            return id is null ? -1 : int.Parse(id); 
        }

        public int GetStoreId()
        {
            var id = contextAccessor.HttpContext?.User?.FindFirst("StoreId")?.Value;
            return id is null ? -1 : int.Parse(id);
        }
        public int GetRoleId()
        {
            var id = contextAccessor.HttpContext?.User?.FindFirst("RoleId")?.Value;
            return id is null ? -1 : int.Parse(id);
        }
    }
}
