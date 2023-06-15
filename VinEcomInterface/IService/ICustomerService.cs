using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
