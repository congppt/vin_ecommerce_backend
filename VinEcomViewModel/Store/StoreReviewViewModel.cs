#nullable disable warnings
using VinEcomViewModel.Customer;

namespace VinEcomViewModel.Store
{
    public class StoreReviewViewModel
    {
        public CustomerBasicViewModel Customer { get; set; }
        public int? Rate { get; set; }
        public string? Comment { get; set; }
    }
}
