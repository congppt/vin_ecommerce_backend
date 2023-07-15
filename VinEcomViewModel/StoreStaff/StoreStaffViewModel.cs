using VinEcomViewModel.Store;

#nullable disable warnings
namespace VinEcomViewModel.StoreStaff
{
    public class StoreStaffViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsBlocked { get; set; }
        public StoreBasicViewModel Store { get; set; }
    }
}
