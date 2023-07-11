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
using VinEcomDomain.Resources;
using VinEcomUtility.Pagination;

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
                AccessToken = accessToken,
                Name = customer.User.Name,
                AvatarUrl = customer.User.AvatarUrl ?? VinEcom.VINECOM_USER_AVATAR_URL_NOT_FOUND
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

        public async Task<CustomerViewModel?> GetCustomerByIdAsync(int id)
        {
            var result = await unitOfWork.CustomerRepository.GetCustomerByIdASync(id);
            return mapper.Map<CustomerViewModel?>(result);
        }

        public async Task<Pagination<CustomerViewModel>> GetCustomerPagesAsync(int pageIndex, int pageSize)
        {
            var result = await unitOfWork.CustomerRepository.GetCustomerPagesAsync(pageIndex, pageSize);
            return mapper.Map<Pagination<CustomerViewModel>>(result);
        }

        public async Task<bool> BlockCustomerAsync(int customerId)
        {
            var customer = await unitOfWork.CustomerRepository.GetCustomerByIdASync(customerId);
            if (customer is null) return false;
            //
            customer.User.IsBlocked = true;
            unitOfWork.CustomerRepository.Update(customer);
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UnblockCustomerAsync(int customerId)
        {
            var customer = await unitOfWork.CustomerRepository.GetCustomerByIdASync(customerId);
            if (customer is null) return false;
            //
            customer.User.IsBlocked = false;
            unitOfWork.CustomerRepository.Update(customer);
            return await unitOfWork.SaveChangesAsync();
        }
    }
}