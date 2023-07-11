using VinEcomDomain.Model;
using VinEcomViewModel.Building;

namespace VinEcomViewModel.Customer
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsBlocked { get; set; }
        public BuildingBasicViewModel Building { get; set; }
    }
}
