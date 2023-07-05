using VinEcomDomain.Enum;

#nullable disable warnings
namespace VinEcomViewModel.Shipper
{
    public class ShipperViewModel
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string VehicleType { get; set; }
        public string Status { get; set; }
        //
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
