using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinEcomViewModel.Product
{
    public class ProductRatingViewModel
    {
        public int ProductId { get; set; }
        public float AverageRating { get; set; }
        public int NumOfRating { get; set; }
    }
}
