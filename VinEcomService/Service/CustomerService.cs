using Microsoft.Extensions.Configuration;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Customer;
using VinEcomViewModel.Global;

namespace VinEcomService.Service
{
    public class CustomerService : UserService, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork, IConfiguration config, ITimeService timeService, ICacheService cacheService) : base(unitOfWork, config, timeService, cacheService)
        {
        }
        #region AuthorizeAsync
        public async Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm)
        {
            var customer = await unitOfWork.CustomerRepository.AuthorizeAsync(vm.Phone, vm.Password);
            if (customer is null) return null;
            string accessToken = customer.User.GenerateToken(config, timeService.GetCurrentTime(), 60 * 24 * 30);
            return new AuthorizedViewModel
            {
                AccessToken = accessToken,
                Name = customer.User.Name
            };
        }
        #endregion
    }
}