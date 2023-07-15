using VinEcomDomain.Enum;
using VinEcomViewModel.Base;
using VinEcomViewModel.Building;

#nullable disable warnings
namespace VinEcomViewModel.Store
{
    public class StoreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public StoreCategoryViewModel Category { get; set; }
        public double CommissionPercent { get; set; }
        public decimal Balance { get; set; }
        public bool IsWorking { get; set; }
        //
        public BuildingBasicViewModel Building { get; set; }
    }
}
