using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.Base;

namespace VinEcomViewModel.Customer
{
    public class CustomerSignUpViewModel : SignUpViewModel
    {
        public int BuildingId { get; set; }
    }
    public class CustomerCreateRule : UserCreateRule<CustomerSignUpViewModel>
    {
        public CustomerCreateRule() : base()
        {
        }
    }
}
