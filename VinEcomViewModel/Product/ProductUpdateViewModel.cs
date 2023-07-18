using FluentValidation;
using VinEcomDomain.Constant;
using VinEcomDomain.Enum;
using VinEcomDomain.Resources;
using VinEcomViewModel.Base;

namespace VinEcomViewModel.Product
{
    public class ProductUpdateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public ProductCategory Category { get; set; }
    }

    public class ProductUpdateValidator : AbstractValidator<ProductUpdateViewModel>
    {
        private readonly int MIN_LENGTH_NAME = int.Parse(VinEcomSettings.Settings["PRODUCT_NAME_MIN_LENGTH"].ToString());
        private readonly int MAX_LENGTH_NAME = int.Parse(VinEcomSettings.Settings["PRODUCT_NAME_MAX_LENGTH"].ToString());

        private string PRODUCT_NAME_ERROR => string.Format(VinEcom.VINECOM_PRODUCT_CREATE_NAME_LENGTH_ERROR, MIN_LENGTH_NAME, MAX_LENGTH_NAME);

        public ProductUpdateValidator()
        {
            //Name
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage(VinEcom.VINECOM_PRODUCT_CREATE_NAME_EMPTY_ERROR)
                .Length(MIN_LENGTH_NAME, MAX_LENGTH_NAME)
                .WithMessage(PRODUCT_NAME_ERROR);
            //Image URL
            RuleFor(x => x.ImageUrl).NotEmpty()
                .WithMessage(VinEcom.VINECOM_IMAGE_URL_EMPTY_ERROR)
                .IsImageUrlAsync();
            //OriginalPrice
            RuleFor(x => x.OriginalPrice).NotEmpty()
                .WithMessage(VinEcom.VINECOM_PRODUCT_CREATE_INVALID_ORIGINAL_PRICE_ERROR)
                .GreaterThan(0)
                .WithMessage(VinEcom.VINECOM_PRODUCT_CREATE_INVALID_ORIGINAL_PRICE_ERROR);
            //DiscountPrice
            RuleFor(x => x.DiscountPrice)
                .GreaterThan(0)
                .WithMessage(VinEcom.VINECOM_PRODUCT_CREATE_INVALID_ORIGINAL_PRICE_ERROR)
                .When(x => x.DiscountPrice.HasValue);
            //Category
            RuleFor(x => x.Category).IsInEnum().WithMessage(VinEcom.VINECOM_PRODUCT_CATEGORY_ERROR);
        }
    }
}
