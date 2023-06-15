using Microsoft.Extensions.Configuration;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Customer;
using VinEcomViewModel.Base;
using VinEcomDomain.Model;
using VinEcomDomain.Resources;
using AutoMapper;

namespace VinEcomService.Service
{
    public class CustomerService : UserService, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork,
                               IConfiguration config,
                               ITimeService timeService,
                               ICacheService cacheService,
                               IClaimService claimService,
                               IMapper mapper) : base(unitOfWork, config, timeService, cacheService, claimService, mapper)
        { }

        public async Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm)
        {
            var customer = await unitOfWork.CustomerRepository.AuthorizeAsync(vm.Phone, vm.Password);
            if (customer is null) return null;
            string accessToken = customer.User.GenerateToken(config, timeService.GetCurrentTime(), 60 * 24 * 30, VinEcom.VINECOM_USER_ROLE_CUSTOMER);
            return new AuthorizedViewModel
            {
                AccessToken = accessToken
            };
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
            try
            {
                if (await unitOfWork.SaveChangesAsync()) return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}