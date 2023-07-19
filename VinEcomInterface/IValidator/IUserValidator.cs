using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.Base;
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
        IValidator<CustomerUpdateBasicViewModel> CustomerUpdateBasicValidator { get; }
        IValidator<UpdatePasswordViewModel> UpdatePasswordValidator { get; }
        IValidator<StoreStaffUpdateViewModel> StaffUpdateValidator { get; }
        IValidator<ShipperUpdateBasicViewModel> ShipperUpdateBasicValidator { get; }
    }
}
