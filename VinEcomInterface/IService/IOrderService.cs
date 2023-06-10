﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
using VinEcomViewModel.OrderDetail;

namespace VinEcomInterface.IService
{
    public interface IOrderService : IBaseService
    {
        Task<bool> AddToCartAsync(AddToCartViewModel vm);
        Task<Pagination<Order>> GetOrdersAsync(int pageIndex, int pageSize);
        Task<bool> IsProductSameStoreAsync(int productId);
    }
}
