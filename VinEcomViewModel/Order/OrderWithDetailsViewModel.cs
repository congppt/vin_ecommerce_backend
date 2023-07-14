using VinEcomDomain.Enum;
using VinEcomViewModel.OrderDetail;
using VinEcomViewModel.Shipper;

#nullable disable warnings
namespace VinEcomViewModel.Order
{
    public class OrderWithDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? TransactionId { get; set; }
        public decimal? ShipFee { get; set; }
        public string Status { get; set; }
        public string Customer { get; set; }
        public string CustomerPhone { get; set; }
        public string FromBuildingName { get; set; }
        public string FromBuildingLocation { get; set; }
        public string ToBuildingName { get; set; }
        public string ToBuildingLocation { get; set; }
        public string StoreName { get; set; }
        public string StoreImageUrl { get; set; }
        public ShipperViewModel Shipper { get; set; }
        public IEnumerable<OrderDetailBasicViewModel> Details { get; set; }
    }
}
