using System;
using System.Collections.Generic;
using VinEcomViewModel.Store;
#nullable disable warnings
namespace VinEcomViewModel.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public bool IsOutOfStock { get; set; }
        public string Category { get; set; }
        //
        public StoreBasicViewModel Store { get; set; }
    }
}
