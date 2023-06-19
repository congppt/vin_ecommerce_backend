using FluentValidation;
using VinEcomInterface.IValidator;
using VinEcomViewModel.Product;

namespace VinEcomOther.Validator
{
    public class ProductValidator : IProductValidator
    {
        private readonly ProductCreateRule _createProductValidator;
        private readonly StoreProductFilterValidator _storeProductFilterValidator;

        public ProductValidator(ProductCreateRule createProductValidator, StoreProductFilterValidator StoreProductFilterValidator)
        {
            _createProductValidator = createProductValidator;
            _storeProductFilterValidator = StoreProductFilterValidator;
        }

        public IValidator<ProductCreateModel> ProductCreateValidator => _createProductValidator;

        public IValidator<StoreProductFilterViewModel> StoreProductFilterValidator => _storeProductFilterValidator;
    }
}
