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
            string accessToken = customer.User.GenerateToken(customer.Id, config, timeService.GetCurrentTime(), 60 * 24 * 30, Role.Customer);
            return new AuthorizedViewModel
            {
                AccessToken = accessToken,
                UserId = customer.User.Id,
                Name = customer.User.Name,
                AvatarUrl = customer.User.AvatarUrl,
                Email = customer.User.Email,
                Phone = customer.User.Phone
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
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<ValidationResult> ValidateRegistrationAsync(CustomerSignUpViewModel vm)
        {
            return await validator.CustomerCreateValidator.ValidateAsync(vm);
        }

        public async Task<CustomerViewModel?> GetCustomerByIdAsync(int id)
        {
            var result = await unitOfWork.CustomerRepository.GetCustomerByIdAsync(id);
            return mapper.Map<CustomerViewModel?>(result);
        }

        public async Task<Pagination<CustomerViewModel>> GetCustomerPagesAsync(int pageIndex, int pageSize)
        {
            var result = await unitOfWork.CustomerRepository.GetCustomerPagesAsync(pageIndex, pageSize);
            return mapper.Map<Pagination<CustomerViewModel>>(result);
        }

        public async Task<bool> UpdateBlockStatusAsync(Customer customer)
        {
            customer.User.IsBlocked = !customer.User.IsBlocked;
            unitOfWork.CustomerRepository.Update(customer);
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<Customer?> FindCustomerAsync(int customerId)
        {
            return await unitOfWork.CustomerRepository.GetCustomerByUserIdAsync(customerId);
        }

        public async Task<CustomerViewModel> GetPersonalInfoAsync()
        {
            var id = claimService.GetRoleId();
            var result = await unitOfWork.CustomerRepository.GetCustomerByIdAsync(id);
            return mapper.Map<CustomerViewModel>(result);
        }

        public async Task<bool> UpdateBasicInfoAsync(CustomerUpdateBasicViewModel vm)
        {
            var id = claimService.GetRoleId();
            var customer = await unitOfWork.CustomerRepository.GetCustomerByIdAsync(id);
            mapper.Map(vm, customer);
            customer.Building = null;
            unitOfWork.CustomerRepository.Update(customer);
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<ValidationResult> ValidateUpdateBasicAsync(CustomerUpdateBasicViewModel vm)
        {
            return await validator.CustomerUpdateBasicValidator.ValidateAsync(vm);
        }
    }
}