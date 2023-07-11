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
using VinEcomUtility.UtilityMethod;
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

        public async Task<bool> IsCorrectCurrentPasswordAsync(UpdatePasswordViewModel vm)
        {
            var userId = claimService.GetCurrentUserId();
            return await unitOfWork.UserRepository.IsPasswordCorrectAsync(userId, vm.CurrentPassword);
        }

        public async Task<bool> IsPhoneExistAsync(string phone)
        {
            var user = await unitOfWork.UserRepository.GetByPhone(phone);
            return user is null ? false : true;
        }

        public async Task<bool> UpdatePasswordAsync(UpdatePasswordViewModel vm)
        {
            var userId = claimService.GetCurrentUserId();
            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);
            user.PasswordHash = vm.NewPassword.BCryptSaltAndHash();
            if (await unitOfWork.SaveChangesAsync()) return true;
            return false;

        }

        public async Task<ValidationResult> ValidateUpdatePasswordAsync(UpdatePasswordViewModel vm)
        {
            return await validator.UpdatePasswordValidator.ValidateAsync(vm);
        }
    }
}
