using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.Base;
using VinEcomViewModel.StoreStaff;

namespace VinEcomInterface.IService
{
    public interface IStoreStaffService : IUserService
    {
        Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm);
        Task<bool> RegisterAsync(StoreStaffSignUpViewModel vm);
        Task<bool> IsStoreExistedAsync(int storeId);
        Task<ValidationResult> ValidateRegistrationAsync(StoreStaffSignUpViewModel vm);
    }
}
