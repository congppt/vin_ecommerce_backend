using FluentValidation;
using FluentValidation.Results;
using VinEcomViewModel.OrderDetail;

namespace VinEcomInterface.IValidator
{
    public interface IOrderValidator
    {
        IValidator<AddToCartViewModel> CartAddValidator { get; }
        IValidator<OrderDetailRatingViewModel> OrderDetailRatingValidator { get; }
    }
}
