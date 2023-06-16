using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomInterface.IValidator;
using VinEcomViewModel.Store;

namespace VinEcomOther.Validator
{
    public class StoreValidator : IStoreValidator
    {
        private readonly StoreCreateValidator storeCreateValidator;
        public StoreValidator(StoreCreateValidator storeCreateValidator)
        {
            this.storeCreateValidator = storeCreateValidator;
        }
        public IValidator<StoreRegisterViewModel> StoreCreateValidator => storeCreateValidator;
    }
}
