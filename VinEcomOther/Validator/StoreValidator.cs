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
        private readonly StoreFilterValidator storeFilterValidator;
        public StoreValidator(StoreCreateValidator storeCreateValidator, StoreFilterValidator storeFilterValidator)
        {
            this.storeCreateValidator = storeCreateValidator;
            this.storeFilterValidator = storeFilterValidator;
        }
        public IValidator<StoreRegisterViewModel> StoreCreateValidator => storeCreateValidator;

        public IValidator<StoreFilterViewModel> StoreFilterValidator => storeFilterValidator;
    }
}
