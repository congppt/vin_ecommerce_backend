using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomInterface.IValidator;
using VinEcomViewModel.Base;
using VinEcomViewModel.Customer;
using VinEcomViewModel.Shipper;
using VinEcomViewModel.StoreStaff;

namespace VinEcomOther.Validator
{
    public class UserValidator : IUserValidator
    {
        private readonly CustomerCreateRule customerCreateValidator;
        private readonly ShipperCreateRule shipperCreateValidator;
        private readonly StaffCreateRule staffCreateValidator;
        private readonly CustomerUpdateBasicRule customerUpdateBasicValidator;
        private readonly UpdatePasswordRule updatePasswordValidator;
        public UserValidator(CustomerCreateRule customerCreateValidator,
                             ShipperCreateRule shipperCreateValidator,
                             StaffCreateRule staffCreateValidator,
                             CustomerUpdateBasicRule customerUpdateBasicValidator,
                             UpdatePasswordRule updatePasswordValidator)
        {
            this.customerCreateValidator = customerCreateValidator;
            this.shipperCreateValidator = shipperCreateValidator;
            this.staffCreateValidator = staffCreateValidator;
            this.customerUpdateBasicValidator = customerUpdateBasicValidator;
            this.updatePasswordValidator = updatePasswordValidator;
        }

        public IValidator<CustomerSignUpViewModel> CustomerCreateValidator => customerCreateValidator;

        public IValidator<ShipperSignUpViewModel> ShipperCreateValidator => shipperCreateValidator;

        public IValidator<StoreStaffSignUpViewModel> StaffCreateValidator => staffCreateValidator;

        public IValidator<CustomerUpdateBasicViewModel> CustomerUpdateBasicValidator => customerUpdateBasicValidator;

        public IValidator<UpdatePasswordViewModel> CustomerUpdatePasswordValidator => updatePasswordValidator;
    }
}
