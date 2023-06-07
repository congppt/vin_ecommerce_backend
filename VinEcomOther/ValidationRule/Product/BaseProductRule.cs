using FluentValidation;
using VinEcomDomain.Enum;
using VinEcomViewModel.Product;

namespace VinEcomOther.ValidationRule.Product
{
    public class BaseProductRule : AbstractValidator<BaseProductViewModel>
    {
        public BaseProductRule()
        {
            RuleFor(x => x.OriginalPrice).GreaterThanOrEqualTo(0).WithMessage("Invalid price");
            RuleFor(x => x.Category).IsInEnum().WithMessage("Invalid Category");
        }
    }
}