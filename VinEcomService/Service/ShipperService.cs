using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Global;

namespace VinEcomService.Service
{
    public class ShipperService : BaseService, IShipperService
    {
        public ShipperService(IUnitOfWork unitOfWork, IConfiguration config, ITimeService timeService, ICacheService cacheService) : base(unitOfWork, config, timeService, cacheService)
        {
        }
        #region AuthorizeAsync
        public async Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm)
        {
            var shipper = await unitOfWork.ShipperRepository.AuthorizeAsync(vm.Phone, vm.Password);
            if (shipper is null) return null;
            string accessToken = shipper.User.GenerateToken(config, timeService.GetCurrentTime(), 60 * 24 * 30);
            return new AuthorizedViewModel
            {
                AccessToken = accessToken,
                Name = shipper.User.Name
            };
        }
        #endregion
    }
}
