using VinEcomDomain.Enum;
using VinEcomViewModel.OrderDetail;
using VinEcomViewModel.Shipper;
using VinEcomViewModel.Building;
using VinEcomViewModel.Store;
using VinEcomViewModel.Customer;
using VinEcomViewModel.Base;

#nullable disable warnings
namespace VinEcomViewModel.Order
{
    public class OrderWithDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? TransactionId { get; set; }
        public decimal? ShipFee { get; set; }
        public OrderStatusViewModel Status { get; set; }
        public CustomerBasicViewModel Customer { get; set; }
        public BuildingBasicViewModel FromBuilding { get; set; }
        public BuildingBasicViewModel ToBuilding { get; set; }
        public StoreBasicViewModel Store { get; set; }
        public ShipperViewModel Shipper { get; set; }
        public IEnumerable<OrderDetailBasicViewModel> Details { get; set; }
    }
}
