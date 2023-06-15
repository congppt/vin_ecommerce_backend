using AutoMapper;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomInterface.IValidator;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Base;
using VinEcomViewModel.Shipper;

namespace VinEcomService.Service
{
    public class ShipperService : UserService, IShipperService
    {
        public ShipperService(IUnitOfWork unitOfWork,
                              IConfiguration config,
                              ITimeService timeService,
                              ICacheService cacheService,
                              IClaimService claimService,
                              IMapper mapper,
                              IUserValidator validator) : base(unitOfWork, config, timeService, cacheService, claimService, mapper, validator)
        {
        }

        public async Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm)
        {
            var shipper = await unitOfWork.ShipperRepository.AuthorizeAsync(vm.Phone, vm.Password);
            if (shipper is null) return null;
            string accessToken = shipper.User.GenerateToken(config, timeService.GetCurrentTime(), 60 * 24 * 30, Role.Shipper);
            return new AuthorizedViewModel
            {
                AccessToken = accessToken
            };
        }

        public async Task<ValidationResult> ValidateRegistrationAsync(ShipperSignUpViewModel vm)
        {
            return await validator.ShipperCreateValidator.ValidateAsync(vm);
        }
        
    }
}
