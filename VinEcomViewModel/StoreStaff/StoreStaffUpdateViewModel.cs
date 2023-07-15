using FluentValidation;
using VinEcomDomain.Constant;
using VinEcomDomain.Resources;
using VinEcomViewModel.Base;

#nullable disable warnings
namespace VinEcomViewModel.StoreStaff
{
    public class StoreStaffUpdateViewModel
    {
        public string Name { get; set; }
        public string? AvatarUrl { get; set; }
        public string Email { get; set; }
    }

    public class StaffUpdateRule : AbstractValidator<StoreStaffUpdateViewModel>
    {
        private readonly int MIN_NAME_LENGTH = int.Parse(VinEcomSettings.Settings["USER_NAME_MIN_LENGTH"].ToString());
        private readonly int MAX_NAME_LENGTH = int.Parse(VinEcomSettings.Settings["USER_NAME_MAX_LENGTH"].ToString());
        private string NAME_LENGTH_ERROR => string.Format(VinEcom.VINECOM_USER_SIGNUP_NAME_LENGTH_ERROR, MIN_NAME_LENGTH, MAX_NAME_LENGTH);

        public StaffUpdateRule()
        {
            RuleFor(x => x.Name).Length(MIN_NAME_LENGTH, MAX_NAME_LENGTH).WithMessage(NAME_LENGTH_ERROR);
            RuleFor(x => x.Email).EmailAddress().WithMessage(VinEcom.VINECOM_EMAIL_INVALID_ERROR).When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.AvatarUrl).IsImageUrlAsync().When(x => !string.IsNullOrWhiteSpace(x.AvatarUrl));
        }
    }
}
