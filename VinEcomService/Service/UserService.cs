using AutoMapper;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomInterface.IValidator;
using VinEcomViewModel.Base;

namespace VinEcomService.Service
{
    public class UserService : BaseService, IUserService
    {
        protected readonly IUserValidator validator;
        public UserService(IUnitOfWork unitOfWork,
                           IConfiguration config,
                           ITimeService timeService,
                           ICacheService cacheService,
                           IClaimService claimService,
                           IMapper mapper,
                           IUserValidator validator) : base(unitOfWork, config, timeService, cacheService, claimService, mapper)
        {
            this.validator = validator;
        }

        public async Task<bool> IsPhoneExistAsync(string phone)
        {
            var user = await unitOfWork.UserRepository.GetByPhone(phone);
            return user is null ? false : true;
        }
    }
}
