using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VinEcomDomain.Constant;
using VinEcomDomain.Enum;
using VinEcomDomain.Resources;

#nullable disable warnings
namespace VinEcomViewModel.Product
{
    public class ProductCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal OriginalPrice { get; set; }
        public ProductCategory Category { get; set; }
    }

    public class ProductCreateRule : AbstractValidator<ProductCreateModel>
    {
        private readonly int MIN_LENGTH_NAME = int.Parse(VinEcomSettings.Settings["PRODUCT_NAME_MIN_LENGTH"].ToString());
        private readonly int MAX_LENGTH_NAME = int.Parse(VinEcomSettings.Settings["PRODUCT_NAME_MAX_LENGTH"].ToString());

        private string PRODUCT_NAME_ERROR => string.Format(VinEcom.VINECOM_PRODUCT_CREATE_NAME_LENGTH_ERROR, MIN_LENGTH_NAME, MAX_LENGTH_NAME);

        public ProductCreateRule()
        {
            //Name
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage(VinEcom.VINECOM_PRODUCT_CREATE_NAME_EMPTY_ERROR)
                .Length(MIN_LENGTH_NAME, MAX_LENGTH_NAME)
                .WithMessage(PRODUCT_NAME_ERROR);
            //Image URL
            RuleFor(x => x.ImageUrl).NotEmpty()
                .WithMessage(VinEcom.VINECOM_IMAGE_URL_EMPTY_ERROR)
                .Must(x => Uri.TryCreate(x, UriKind.Absolute, out var validateResult) && (validateResult.Scheme == Uri.UriSchemeHttp || validateResult.Scheme == Uri.UriSchemeHttps))
                .WithMessage(VinEcom.VINECOM_IMAGE_URL_FORMAT_ERROR);
            //OriginalPrice
            RuleFor(x => x.OriginalPrice).NotEmpty()
                .WithMessage(VinEcom.VINECOM_PRODUCT_CREATE_INVALID_ORIGINAL_PRICE_ERROR)
                .GreaterThan(0)
                .WithMessage(VinEcom.VINECOM_PRODUCT_CREATE_INVALID_ORIGINAL_PRICE_ERROR);
            //Category
            RuleFor(x => x.Category).IsInEnum().WithMessage(VinEcom.VINECOM_PRODUCT_CREATE_CATEGORY_ERROR);
        }
    }
}
