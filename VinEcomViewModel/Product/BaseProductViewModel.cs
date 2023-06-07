using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinEcomViewModel.Product
{
    public class BaseProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Category { get; set; }
        //
        public int StoreId { get; set; }
    }
}
