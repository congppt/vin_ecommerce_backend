using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Base;
using VinEcomViewModel.Order;
using VinEcomViewModel.Shipper;
using VinEcomViewModel.StoreStaff;

namespace VinEcomInterface.IService
{
    public interface IShipperService : IUserService
    {
        Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm);
        Task<bool> RegisterAsync(ShipperSignUpViewModel vm);
        Task<ValidationResult> ValidateRegistrationAsync(ShipperSignUpViewModel vm);
        Task<IEnumerable<ShipperViewModel>> GetShippersAvailableAsync();
        Task<IEnumerable<OrderWithDetailsViewModel>?> GetDeliveredListAsync();
        Task<bool> ChangeWorkingStatusAsync();
        Task<bool> OrderDeliveredAsync();
        Task<bool> ReceiveOrderAsync(int orderId);
        Task<ValidationResult> ValidateUpdateBasicAsync(ShipperUpdateBasicViewModel vm);
        Task<bool> UpdateBasicAsync(ShipperUpdateBasicViewModel vm);
        Task<ShipperViewModel> GetInfoAsync();
        Task<Pagination<ShipperViewModel>> GetShipperPageAsync(int pageIndex, int pageSize);
    }
}
