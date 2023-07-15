using FluentValidation;
using VinEcomInterface.IValidator;
using VinEcomViewModel.OrderDetail;

namespace VinEcomOther.Validator
{
    public class OrderValidator : IOrderValidator
    {
        private readonly CartAddValidator _cartValidator;
        private readonly OrderDetailRatingRule _orderDetailRatingValidator;

        public OrderValidator(CartAddValidator cartValidator, OrderDetailRatingRule orderDetailRatingValidator)
        {
            _cartValidator = cartValidator;
            _orderDetailRatingValidator = orderDetailRatingValidator;
        }

        public IValidator<AddToCartViewModel> CartAddValidator => _cartValidator;

        public IValidator<OrderDetailRatingViewModel> OrderDetailRatingValidator => _orderDetailRatingValidator;
    }
}
