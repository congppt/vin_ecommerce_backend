using VinEcomDomain.Enum;

#nullable disable warnings
namespace VinEcomViewModel.Order
{
    public class OrderWithDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? TransactionId { get; set; }
        public decimal? ShipFee { get; set; }
        public OrderStatus Status { get; set; }
        public string Customer { get; set; }
        public string Shipper { get; set; }
        public IEnumerable<OrderDetailViewModel> Details { get; set; }
    }
}
