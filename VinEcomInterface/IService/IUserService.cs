using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.Base;

namespace VinEcomInterface.IService
{
    public interface IUserService : IBaseService
    {
        Task<AuthorizedViewModel?> AdminAuthorizeAsync(SignInViewModel vm);
        Task<bool> IsPhoneExistAsync(string phone);
        Task<ValidationResult> ValidateUpdatePasswordAsync(UpdatePasswordViewModel vm);
        Task<bool> UpdatePasswordAsync(UpdatePasswordViewModel vm);
        Task<bool> IsCorrectCurrentPasswordAsync(UpdatePasswordViewModel vm);
    }
}
