using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.OrderDetail;

namespace VinEcomInterface.IService
{
    public interface IOrderService : IBaseService
    {
        Task<bool> AddToCartAsync(AddToCartViewModel vm);
    }
}
