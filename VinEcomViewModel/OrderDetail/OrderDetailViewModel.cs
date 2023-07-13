using VinEcomViewModel.Customer;
using VinEcomViewModel.Product;

namespace VinEcomViewModel.OrderDetail
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Comment { get; set; }
        public int? Rate { get; set; }
        //
        public CustomerBasicViewModel Customer { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
