using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;

namespace VinEcomDomain.Model
{
    public class Shipper
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public VehicleType VehicleType { get; set; }
        public ShipperStatus Status { get; set; }
        //
        public int UserId { get; set; }
        public User User { get; set; }
        //
        public ICollection<Order> Orders { get; set; }
    }
}
