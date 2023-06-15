using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomInterface.IValidator;
using VinEcomOther.ValidationRule.Product;
using VinEcomOther.ValidationRule.User;
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
        public UserValidator(CustomerCreateRule customerCreateValidator,
                             ShipperCreateRule shipperCreateValidator,
                             StaffCreateRule staffCreateValidator)
        {
            this.customerCreateValidator = customerCreateValidator;
            this.shipperCreateValidator = shipperCreateValidator;
            this.staffCreateValidator = staffCreateValidator;
        }

        public IValidator<CustomerSignUpViewModel> CustomerCreateValidator => customerCreateValidator;

        public IValidator<ShipperSignUpViewModel> ShipperCreateValidator => shipperCreateValidator;

        public IValidator<StoreStaffSignUpViewModel> StaffCreateValidator => staffCreateValidator;
    }
}
