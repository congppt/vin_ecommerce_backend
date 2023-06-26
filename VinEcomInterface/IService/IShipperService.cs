using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
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
        Task<IEnumerable<Shipper>> GetShippersAvailableAsync();
        Task<IEnumerable<Order>?> GetDeliveredListAsync();
        Task<bool> ChangeWorkingStatusAsync();
        Task<bool> OrderDeliveredAsync();
    }
}
