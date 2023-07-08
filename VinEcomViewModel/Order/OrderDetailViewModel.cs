namespace VinEcomViewModel.Order
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Comment { get; set; }
        public int? Rate { get; set; }
    }
}
