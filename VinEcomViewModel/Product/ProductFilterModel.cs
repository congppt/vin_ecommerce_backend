using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Resources;

#nullable disable warnings
namespace VinEcomViewModel.Product
{
    public class ProductFilterModel
    {
        public ProductCategory? Category { get; set; }
        public int? StoreId { get; set; }
        public string? Name { get; set; }
    }

    public class ProductFilterValidator : AbstractValidator<ProductFilterModel>
    {
        public ProductFilterValidator()
        {
            //Category
            RuleFor(x => x.Category).IsInEnum().WithMessage(VinEcom.VINECOM_PRODUCT_CATEGORY_ERROR);
        }
    }
}
