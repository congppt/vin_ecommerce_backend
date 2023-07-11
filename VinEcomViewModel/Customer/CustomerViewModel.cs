using VinEcomDomain.Model;

namespace VinEcomViewModel.Customer
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        //
        public int UserId { get; set; }
        public int BuildingId { get; set; }
        public string BuidingName { get; set; }
        //
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsBlocked { get; set; }
    }
}
