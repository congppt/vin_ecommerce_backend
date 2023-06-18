using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Constant;
using VinEcomDomain.Enum;
using VinEcomDomain.Resources;

namespace VinEcomViewModel.Store
{
    public class StoreRegisterViewModel
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public StoreCategory Category { get; set; }
        public int CommissionPercent { get; set; }
        public int BuildingId { get; set; }
    }
    public class StoreCreateValidator : AbstractValidator<StoreRegisterViewModel>
    {
        private readonly int MIN_NAME_LENGTH = int.Parse(VinEcomSettings.Settings["STORE_NAME_MIN_LENGTH"].ToString());
        private readonly int MAX_NAME_LENGTH = int.Parse(VinEcomSettings.Settings["STORE_NAME_MAX_LENGTH"].ToString());
        private readonly int MIN_COMMISSION_PERCENT = int.Parse(VinEcomSettings.Settings["MIN_COMMISSION_PERCENT"].ToString());
        private readonly int MAX_COMMISSION_PERCENT = int.Parse(VinEcomSettings.Settings["MAX_COMMISSION_PERCENT"].ToString());
        private string NAME_LENGTH_ERROR => string.Format(VinEcom.VINECOM_STORE_REGISTER_NAME_LENGTH_ERROR, MIN_NAME_LENGTH, MAX_NAME_LENGTH);
        private string COMMISSION_PERCENT_RANGE_ERROR => string.Format(VinEcom.VINECOM_STORE_REGISTER_COMMISSION_RANGE_ERROR, MIN_COMMISSION_PERCENT, MAX_COMMISSION_PERCENT);
        public StoreCreateValidator(){
            RuleFor(x => x.Name).Length(MIN_NAME_LENGTH, MAX_NAME_LENGTH).WithMessage(NAME_LENGTH_ERROR);
            RuleFor(x => x.ImageUrl).NotEmpty()
                                    .WithMessage(VinEcom.VINECOM_IMAGE_URL_EMPTY_ERROR)
                                    .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                                    .WithMessage(VinEcom.VINECOM_IMAGE_URL_FORMAT_ERROR);
            RuleFor(x => x.Category).IsInEnum().WithMessage(VinEcom.VINECOM_STORE_CATEGORY_NOT_EXIST_ERROR);
            RuleFor(x => x.CommissionPercent).InclusiveBetween(MIN_COMMISSION_PERCENT,MAX_COMMISSION_PERCENT).WithMessage(COMMISSION_PERCENT_RANGE_ERROR);
        }
    }
}
