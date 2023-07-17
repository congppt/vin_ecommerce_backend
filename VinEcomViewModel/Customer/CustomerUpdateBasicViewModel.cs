using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Constant;
using VinEcomDomain.Resources;
using VinEcomViewModel.Base;

namespace VinEcomViewModel.Customer
{
    public class CustomerUpdateBasicViewModel
    {
        public string Name { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Email { get; set; }
        public int BuildingId { get; set; }
    }
    public class CustomerUpdateBasicRule : AbstractValidator<CustomerUpdateBasicViewModel>
    {
        private readonly int MIN_NAME_LENGTH = int.Parse(VinEcomSettings.Settings["USER_NAME_MIN_LENGTH"].ToString());
        private readonly int MAX_NAME_LENGTH = int.Parse(VinEcomSettings.Settings["USER_NAME_MAX_LENGTH"].ToString());
        private string NAME_LENGTH_ERROR => string.Format(VinEcom.VINECOM_USER_SIGNUP_NAME_LENGTH_ERROR, MIN_NAME_LENGTH, MAX_NAME_LENGTH);
        public CustomerUpdateBasicRule()
        {
            RuleFor(x => x.Name).Length(MIN_NAME_LENGTH, MAX_NAME_LENGTH)
                                .WithMessage(NAME_LENGTH_ERROR);
            RuleFor(x => x.Email).EmailAddress().WithMessage(VinEcom.VINECOM_EMAIL_INVALID_ERROR).When(x => !string.IsNullOrWhiteSpace(x.Email));
            RuleFor(x => x.AvatarUrl).IsImageUrlAsync().When(x => !string.IsNullOrWhiteSpace(x.AvatarUrl));
        }
    }
}
