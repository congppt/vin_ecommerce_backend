using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Resources;

namespace VinEcomViewModel.OrderDetail
{
    public class AddToCartViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CartAddValidator : AbstractValidator<AddToCartViewModel>
    {
        public CartAddValidator()
        {
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1).WithMessage(VinEcom.VINECOM_ORDER_ADDTOCART_QUANTITY_ERROR);
        }
    }
}
