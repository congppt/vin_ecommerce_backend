using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Constant;
using VinEcomDomain.Resources;

namespace VinEcomViewModel.Base
{
    public class UpdatePasswordViewModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class UpdatePasswordRule : AbstractValidator<UpdatePasswordViewModel>
    {
        private readonly int MIN_PASSWORD_LENGTH = int.Parse(VinEcomSettings.Settings["USER_PASSWORD_MIN_LENGTH"].ToString());
        private readonly int MAX_PASSWORD_LENGTH = int.Parse(VinEcomSettings.Settings["USER_PASSWORD_MAX_LENGTH"].ToString());
        private string PASSWORD_LENGTH_ERROR => string.Format(VinEcom.VINECOM_USER_SIGNUP_PASSWORD_LENGTH_ERROR, MIN_PASSWORD_LENGTH, MAX_PASSWORD_LENGTH);
        public UpdatePasswordRule()
        {
            RuleFor(x => x.NewPassword).Length(MIN_PASSWORD_LENGTH, MAX_PASSWORD_LENGTH)
                                    .WithMessage(PASSWORD_LENGTH_ERROR);
        }
    }
}
