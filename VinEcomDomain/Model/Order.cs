using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;

namespace VinEcomDomain.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? TransactionId { get; set; }
        public decimal? ShipFee { get; set; }
        public OrderStatus Status { get; set; }
        //
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        //
        public int? BuildingId { get; set; }
        public Building Building { get; set; }
        //
        public int? ShipperId { get; set; }
        public Shipper Shipper { get; set; }
        //
        public ICollection<OrderDetail> Details { get; set; }
    }
}
