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
        private readonly ProductUpdateValidator _productUpdateValidator;

        public ProductValidator(ProductCreateRule createProductValidator, 
            StoreProductFilterValidator storeProductFilterValidator, 
            ProductFilterValidator productFilterValidator, 
            ProductUpdateValidator productUpdateValidator)
        {
            _createProductValidator = createProductValidator;
            _storeProductFilterValidator = storeProductFilterValidator;
            _productFilterValidator = productFilterValidator;
            _productUpdateValidator = productUpdateValidator;
        }

        public IValidator<ProductCreateModel> ProductCreateValidator => _createProductValidator;

        public IValidator<StoreProductFilterViewModel> StoreProductFilterValidator => _storeProductFilterValidator;
        public IValidator<ProductFilterModel> ProductFilterValidator => _productFilterValidator;

        public IValidator<ProductUpdateViewModel> ProductUpdateValidator => _productUpdateValidator;
    }
}
