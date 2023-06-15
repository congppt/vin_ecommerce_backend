using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.Customer;

namespace VinEcomOther.ValidationRule.User
{
    public class CustomerCreateRule : UserCreateRule<CustomerSignUpViewModel>
    {
        public CustomerCreateRule():base()
        {
        }
    }
}
