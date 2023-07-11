using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Base;
using VinEcomViewModel.Customer;

namespace VinEcomInterface.IService
{
    public interface ICustomerService : IUserService
    {
        Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm);
        Task<bool> RegisterAsync(CustomerSignUpViewModel vm);
        Task<bool> IsBuildingExistedAsync(int buildingId);
        Task<ValidationResult> ValidateRegistrationAsync(CustomerSignUpViewModel vm);
        Task<CustomerViewModel?> GetCustomerByIdAsync(int id);
        Task<Pagination<CustomerViewModel>> GetCustomerPagesAsync(int pageIndex, int pageSize);
        Task<bool> UpdateBlockStatusAsync(Customer customer);
        Task<Customer?> FindCustomerAsync(int customerId);
        Task<CustomerViewModel> GetPersonalInfoAsync();
        Task<ValidationResult> ValidateUpdateBasicAsync(CustomerUpdateBasicViewModel vm);
        Task<bool> UpdateBasicInfoAsync(CustomerUpdateBasicViewModel vm);
    }
}
