using FluentValidation;
using VinEcomDomain.Constant;
using VinEcomDomain.Resources;

namespace VinEcomViewModel.OrderDetail
{
    public class OrderDetailRatingViewModel
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public int? Rate { get; set; }
    }

    public class OrderDetailRatingRule : AbstractValidator<OrderDetailRatingViewModel>
    {
        private readonly int RATE_MIN_VALUE = int.Parse(VinEcomSettings.Settings["RATING_MIN_VALUE"].ToString());
        private readonly int RATE_MAX_VALUE = int.Parse(VinEcomSettings.Settings["RATING_MAX_VALUE"].ToString());

        private string RATE_VALUE_ERROR => string.Format(VinEcom.VINECOM_RATING_VALUE_ERROR, RATE_MIN_VALUE, RATE_MAX_VALUE);

        public OrderDetailRatingRule()
        {
            RuleFor(x => x.Rate).InclusiveBetween(RATE_MIN_VALUE, RATE_MAX_VALUE)
                .WithMessage(RATE_VALUE_ERROR).When(x => x.Rate.HasValue);
        }
    }
}
