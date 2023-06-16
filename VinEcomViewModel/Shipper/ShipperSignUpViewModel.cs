using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Resources;
using VinEcomViewModel.Base;

namespace VinEcomViewModel.Shipper
{
    public class ShipperSignUpViewModel : SignUpViewModel
    {
        public VehicleType VehicleType { get; set; }
        public string LicensePlate { get; set; }
    }
    public class ShipperCreateRule : UserCreateRule<ShipperSignUpViewModel>
    {
        public ShipperCreateRule() : base()
        {
            RuleFor(x => x.VehicleType).IsInEnum().WithMessage(VinEcom.VINECOM_VEHICLE_TYPE_NOT_EXIST);
            RuleFor(x => x.LicensePlate).NotEmpty().WithMessage(VinEcom.VINECOM_SHIPPER_SIGNUP_PLATE_EMPTY_ERROR);
        }
    }
}
