using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomViewModel.Base;

namespace VinEcomViewModel.Shipper
{
    public class ShipperSignUpViewModel : SignUpViewModel
    {
        public VehicleType VehicleType { get; set; }
        public string LicensePlate { get; set; }
    }
}
