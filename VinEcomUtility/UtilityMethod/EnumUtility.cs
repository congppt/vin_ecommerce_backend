using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Resources;

namespace VinEcomUtility.UtilityMethod
{
    public static class EnumUtility
    {
        public static Dictionary<int, string> GetEnumDictionary(this Type enumType, Func<object, string> getDisplayName)
        {
            var dict = new Dictionary<int, string>();
            foreach (var value in Enum.GetValues(enumType))
            {
                dict.Add((int)value, getDisplayName(value));
            }
            return dict;
        }
        public static string GetDisplayName(this ProductCategory category)
        {
            return category switch
            {
                ProductCategory.Food => VinEcom.VINECOM_PRODUCT_CATEGORY_FOOD,
                ProductCategory.Beverage => VinEcom.VINECOM_PRODUCT_CATEGORY_BEVERAGE,
                ProductCategory.Necessity => VinEcom.VINECOM_PRODUCT_CATEGORY_NECESSITY,
                _ => "",
            };
        }
        public static string GetDisplayName(this OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Cart => "",
                OrderStatus.Preparing => VinEcom.VINECOM_ORDER_STATUS_PREPARING,
                OrderStatus.Cancel => VinEcom.VINECOM_ORDER_STATUS_CANCEL,
                OrderStatus.Done => VinEcom.VINECOM_ORDER_STATUS_DONE,
                OrderStatus.Shipping => VinEcom.VINECOM_ORDER_STATUS_SHIPPING,
                _ => "",
            };
        }
        public static string GetDisplayName(this StoreCategory category)
        {
            return category switch
            {
                StoreCategory.Food => VinEcom.VINECOM_STORE_CATEGORY_FOOD,
                StoreCategory.Beverage => VinEcom.VINECOM_STORE_CATEGORY_BEVERAGE,
                StoreCategory.Grocery =>VinEcom.VINECOM_STORE_CATEGORY_GROCERY,
                _ => "",
            };
        }
        public static string GetDisplayName(this VehicleType type)
        {
            return type switch
            {
                VehicleType.Motorbike => VinEcom.VINECOM_VEHICLE_TYPE_MOTORBIKE,
                _ => "",
            };
        }
        public static string GetDisplayName(this ShipperStatus status)
        {
            return status switch
            {
                ShipperStatus.Offline => VinEcom.VINECOM_SHIPPER_STATUS_OFFLINE,
                ShipperStatus.Available => VinEcom.VINECOM_SHIPPER_STATUS_AVAILABLE,
                ShipperStatus.Enroute => VinEcom.VINECOM_SHIPPER_STATUS_ENROUTE,
                _ => "",
            };
        }
    }
}
