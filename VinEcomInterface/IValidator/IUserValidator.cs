using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.Customer;
using VinEcomViewModel.Shipper;
using VinEcomViewModel.StoreStaff;

namespace VinEcomInterface.IValidator
{
    public interface IUserValidator
    {
        IValidator<CustomerSignUpViewModel> CustomerCreateValidator { get; }
        IValidator<ShipperSignUpViewModel> ShipperCreateValidator { get; }
        IValidator<StoreStaffSignUpViewModel> StaffCreateValidator { get; }
    }
}
