using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;

namespace VinEcomUtility.UtilityMethod
{
    public static class EnumUtility
    {
        public static string GetDisplayName(this ProductCategory category)
        {
            return category switch
            {
                ProductCategory.Food => "",
                ProductCategory.Drink => "",
                ProductCategory.Necessity => "",
                _ => "",
            };
        }
        public static string GetDisplayName(this OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Cart => "",
                OrderStatus.CartCancel => "",
                OrderStatus.Preparing => "",
                OrderStatus.Cancel => "",
                OrderStatus.Done => "",
                OrderStatus.Shipping => "",
                _ => "",
            };
        }
        public static string GetDisplayName(this StoreCategory category)
        {
            return category switch
            {
                StoreCategory.Food => "",
                StoreCategory.Grocery => "",
                _ => "",
            };
        }
        public static string GetDisplayName(this VehicleType type)
        {
            return type switch
            {
                VehicleType.Motorbike => "",
                _ => "",
            };
        }
    }
}
