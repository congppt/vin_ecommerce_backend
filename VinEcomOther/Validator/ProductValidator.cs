using FluentValidation;
using VinEcomInterface.IValidator;
using VinEcomOther.ValidationRule.Product;
using VinEcomViewModel.Product;

namespace VinEcomOther.Validator
{
    public class ProductValidator : IProductValidator
    {
        private readonly ProductCreateRule _createProductValidator;

        public ProductValidator(ProductCreateRule createProductValidator)
        {
            _createProductValidator = createProductValidator;
        }

        public IValidator<ProductCreateModel> ProductCreateValidator => _createProductValidator;
    }
}
