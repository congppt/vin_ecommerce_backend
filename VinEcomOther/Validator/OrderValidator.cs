using FluentValidation;
using VinEcomInterface.IValidator;
using VinEcomViewModel.OrderDetail;

namespace VinEcomOther.Validator
{
    public class OrderValidator : IOrderValidator
    {
        private readonly CartAddValidator _cartValidator;

        public OrderValidator(CartAddValidator cartValidation)
        {
            _cartValidator = cartValidation;
        }

        public IValidator<AddToCartViewModel> CartAddValidator => _cartValidator;
    }
}
