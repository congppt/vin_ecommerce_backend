using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomViewModel.Base;

namespace VinEcomService.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork,
                           IConfiguration config,
                           ITimeService timeService,
                           ICacheService cacheService,
                           IClaimService claimService,
                           IMapper mapper) : base(unitOfWork, config, timeService, cacheService, claimService, mapper)
        {
        }

        public async Task<bool> IsPhoneExist(string phone)
        {
            var user = await unitOfWork.UserRepository.GetByPhone(phone);
            return user is null ? false : true;
        }
    }
}
