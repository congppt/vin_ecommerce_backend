using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;

namespace VinEcomDomain.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }  
        public decimal OriginalPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public bool IsOutOfStock { get; set; }
        public bool IsRemoved { get; set; }
        public ProductCategory Category { get; set; }
        //
        public int StoreId { get; set; }
        public Store Store { get; set; }
        //
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
