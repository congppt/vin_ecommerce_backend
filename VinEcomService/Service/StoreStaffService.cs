using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Resources;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Base;

namespace VinEcomService.Service
{
    public class StoreStaffService : UserService, IStoreStaffService
    {
        public StoreStaffService(IUnitOfWork unitOfWork,
                                 IConfiguration config,
                                 ITimeService timeService,
                                 ICacheService cacheService,
                                 IClaimService claimService,
                                 IMapper mapper) : base(unitOfWork, config, timeService, cacheService, claimService, mapper)
        {
        }
        #region AuthorizeAsync
        public async Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm)
        {
            var storeStaff = await unitOfWork.StoreStaffRepository.AuthorizeAsync(vm.Phone, vm.Password);
            if (storeStaff is null) return null;
            string accessToken = storeStaff.User.GenerateToken(config, timeService.GetCurrentTime(), 60 * 24 * 30, VinEcom.VINECOM_USER_ROLE_STORESTAFF, storeStaff.StoreId);
            return new AuthorizedViewModel
            {
                AccessToken = accessToken
            };
        }
        #endregion
    }
}
