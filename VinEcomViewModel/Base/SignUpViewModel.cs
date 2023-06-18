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
    public class SignUpViewModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
    public class UserCreateRule<T> : AbstractValidator<T> where T : SignUpViewModel
    {
        private readonly int MIN_NAME_LENGTH = int.Parse(VinEcomSettings.Settings["USER_NAME_MIN_LENGTH"].ToString());
        private readonly int MAX_NAME_LENGTH = int.Parse(VinEcomSettings.Settings["USER_NAME_MAX_LENGTH"].ToString());
        private readonly int MIN_PASSWORD_LENGTH = int.Parse(VinEcomSettings.Settings["USER_PASSWORD_MIN_LENGTH"].ToString());
        private readonly int MAX_PASSWORD_LENGTH = int.Parse(VinEcomSettings.Settings["USER_PASSWORD_MAX_LENGTH"].ToString());
        private string PASSWORD_LENGTH_ERROR => string.Format(VinEcom.VINECOM_USER_SIGNUP_PASSWORD_LENGTH_ERROR, MIN_PASSWORD_LENGTH, MAX_PASSWORD_LENGTH);
        private string NAME_LENGTH_ERROR => string.Format(VinEcom.VINECOM_USER_SIGNUP_NAME_LENGTH_ERROR, MIN_NAME_LENGTH, MAX_NAME_LENGTH);
        private const string PHONE_REGEX = @"(84|0[3|5|7|8|9])+([0-9]{8})\b";
        public UserCreateRule()
        {
            RuleFor(x => x.Phone).Matches(PHONE_REGEX)
                                 .WithMessage(VinEcom.VINECOM_USER_SIGNUP_PHONE_FORMAT_ERROR);
            RuleFor(x => x.Name).Length(MIN_NAME_LENGTH, MAX_NAME_LENGTH)
                                .WithMessage(NAME_LENGTH_ERROR);
            RuleFor(x => x.Password).Length(MIN_PASSWORD_LENGTH, MAX_PASSWORD_LENGTH)
                                    .WithMessage(PASSWORD_LENGTH_ERROR);
        }
    }
}
