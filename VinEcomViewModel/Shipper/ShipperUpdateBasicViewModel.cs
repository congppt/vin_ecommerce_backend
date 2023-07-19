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
    public class ShipperUpdateBasicViewModel : UserUpdateBasicViewModel
    {
        public string LicensePlate { get; set; }
        public VehicleType VehicleType { get; set; }
    }
    public class ShipperUpdateBasicRule : UserUpdateBasicRule<ShipperUpdateBasicViewModel>
    {
        public ShipperUpdateBasicRule() : base()
        {
            RuleFor(x => x.VehicleType).IsInEnum().WithMessage(VinEcom.VINECOM_VEHICLE_TYPE_NOT_EXIST);
        }
    }
}
