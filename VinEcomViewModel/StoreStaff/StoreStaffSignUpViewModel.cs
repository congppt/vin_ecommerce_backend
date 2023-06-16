using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.Base;

namespace VinEcomViewModel.StoreStaff
{
    public class StoreStaffSignUpViewModel : SignUpViewModel
    {
        public int StoreId { get; set; }
    }
    public class StaffCreateRule : UserCreateRule<StoreStaffSignUpViewModel>
    {
        public StaffCreateRule() : base()
        {
        }
    }
}
