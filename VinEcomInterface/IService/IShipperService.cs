using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.Base;
using VinEcomViewModel.Shipper;
using VinEcomViewModel.StoreStaff;

namespace VinEcomInterface.IService
{
    public interface IShipperService : IUserService
    {
        Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm);
        Task<bool> RegisterAsync(ShipperSignUpViewModel vm);
        Task<ValidationResult> ValidateRegistrationAsync(ShipperSignUpViewModel vm);
    }
}
