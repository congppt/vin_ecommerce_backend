using FluentValidation;
using VinEcomInterface.IValidator;
using VinEcomViewModel.Product;

namespace VinEcomOther.Validator
{
    public class ProductValidator : IProductValidator
    {
        private readonly ProductCreateRule _createProductValidator;
        private readonly StoreProductFilterValidator _storeProductFilterValidator;
        private readonly ProductFilterValidator _productFilterValidator;

        public ProductValidator(ProductCreateRule createProductValidator, 
            StoreProductFilterValidator storeProductFilterValidator, 
            ProductFilterValidator productFilterValidator)
        {
            _createProductValidator = createProductValidator;
            _storeProductFilterValidator = storeProductFilterValidator;
            _productFilterValidator = productFilterValidator;
        }

        public IValidator<ProductCreateModel> ProductCreateValidator => _createProductValidator;

        public IValidator<StoreProductFilterViewModel> StoreProductFilterValidator => _storeProductFilterValidator;
        public IValidator<ProductFilterModel> ProductFilterValidator => _productFilterValidator;
    }
}
