using Microsoft.Extensions.Configuration;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Customer;
using VinEcomViewModel.Base;
using VinEcomDomain.Model;
using VinEcomDomain.Resources;

namespace VinEcomService.Service
{
    public class CustomerService : UserService, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork,
            IConfiguration config, ITimeService timeService, 
            ICacheService cacheService, IClaimService claimService) : base(unitOfWork, config, timeService, cacheService, claimService)
        { }

        public async Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm)
        {
            var customer = await unitOfWork.CustomerRepository.AuthorizeAsync(vm.Phone, vm.Password);
            if (customer is null) return null;
            string accessToken = customer.User.GenerateToken(config, timeService.GetCurrentTime(), 60 * 24 * 30, VinEcom.VINECOM_USER_ROLE_CUSTOMER);
            return new AuthorizedViewModel
            {
                AccessToken = accessToken,
                Name = customer.User.Name
            };
        }

        public async Task<bool> RegisterAsync(CustomerSignUpViewModel vm)
        {
            var user = new User
            {
                Name = vm.Name,
                PasswordHash = vm.Password.BCryptSaltAndHash(),
                IsBlocked = false,
                Phone = vm.Phone,
            };
            var customer = new Customer
            {
                User = user,
                BuildingId = vm.BuildingId,
            };
            await unitOfWork.CustomerRepository.AddAsync(customer);
            if (await unitOfWork.SaveChangesAsync()) return true;
            return false;
        }
    }
}