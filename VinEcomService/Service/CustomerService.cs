using Microsoft.Extensions.Configuration;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Customer;
using VinEcomViewModel.Base;
using VinEcomDomain.Model;
using AutoMapper;
using VinEcomInterface.IValidator;
using VinEcomDomain.Enum;
using FluentValidation.Results;

namespace VinEcomService.Service
{
    public class CustomerService : UserService, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork,
                               IConfiguration config,
                               ITimeService timeService,
                               ICacheService cacheService,
                               IClaimService claimService,
                               IMapper mapper,
                               IUserValidator validator) : base(unitOfWork, config, timeService, cacheService, claimService, mapper, validator)
        { }

        public async Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm)
        {
            var customer = await unitOfWork.CustomerRepository.AuthorizeAsync(vm.Phone, vm.Password);
            if (customer is null) return null;
            string accessToken = customer.User.GenerateToken(config, timeService.GetCurrentTime(), 60 * 24 * 30, Role.Customer);
            return new AuthorizedViewModel
            {
                AccessToken = accessToken
            };
        }

        public async Task<bool> IsBuildingExistedAsync(int buildingId)
        {
            return await unitOfWork.BuildingRepository.GetByIdAsync(buildingId) is not null;
        }

        public async Task<bool> RegisterAsync(CustomerSignUpViewModel vm)
        {
            var user = mapper.Map<User>(vm);
            user.PasswordHash = vm.Password.BCryptSaltAndHash();
            var customer = new Customer
            {
                User = user,
                BuildingId = vm.BuildingId,
            };
            await unitOfWork.CustomerRepository.AddAsync(customer);
            if (await unitOfWork.SaveChangesAsync()) return true;
            return false;
        }

        public async Task<ValidationResult> ValidateRegistrationAsync(CustomerSignUpViewModel vm)
        {
            return await validator.CustomerCreateValidator.ValidateAsync(vm);
        }
    }
}