using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;

namespace VinEcomDomain.Model
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string? ImageUrl { get; set; }
        public StoreCategory Category { get; set; }
        public double CommissionPercent { get; set; }
        public decimal Balance { get; set; }
        public bool IsWorking { get; set; }
        public bool IsBlocked { get; set; }
        //
        public int BuildingId { get; set; }
        public Building Building { get; set; }
        //
        public ICollection<Product> Products { get; set; }
        public ICollection<StoreStaff> Staffs { get; set; }
    }
}
