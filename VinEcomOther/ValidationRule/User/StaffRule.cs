using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.StoreStaff;

namespace VinEcomOther.ValidationRule.User
{
    public class StaffCreateRule : UserCreateRule<StoreStaffSignUpViewModel>
    {
        public StaffCreateRule():base()
        {
        }
    }
}
