#nullable disable warnings
namespace VinEcomViewModel.Store
{
    public class StoreReviewViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerAvatarUrl { get; set; }
        public int? Rate { get; set; }
        public string? Comment { get; set; }
    }
}
