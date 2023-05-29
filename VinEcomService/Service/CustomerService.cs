using Microsoft.Extensions.Configuration;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Account;

namespace VinEcomService.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly ITimeService timeService;
        private readonly ICacheService cacheService;
        public CustomerService(IUnitOfWork unitOfWork, IConfiguration config, ITimeService timeService, ICacheService cacheService)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.timeService = timeService;
            this.cacheService = cacheService;
        }
        #region AuthorizeAsync
        public async Task<CustomerAuthorizedViewModel?> AuthorizeAsync(string phone, string password)
        {
            var customer =  await unitOfWork.CustomerRepository.AuthorizeAsync(phone, password);
            if (customer is null) return null;
            string accessToken = customer.User.GenerateToken(config, timeService.GetCurrentTime(), 60 * 24 * 30);
            return new CustomerAuthorizedViewModel
            {
                AccessToken = accessToken,
                Name = customer.User.Name
            };
        }
        #endregion
    }
}