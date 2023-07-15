#nullable disable warnings
using VinEcomViewModel.Building;

namespace VinEcomViewModel.Order
{
    public class OrderStoreViewModel
    {
        public int Id { get; set; }
        public BuildingBasicViewModel CustomerBuilding { get; set; }
        public BuildingBasicViewModel StoreBuilding { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
    }
}
