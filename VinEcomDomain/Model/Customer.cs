using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinEcomDomain.Model
{
    public class Customer
    {
        public int Id { get; set; }
        //
        public int UserId { get; set; }
        public User User { get; set; }
        //
        public int BuildingId { get; set; }
        public Building Building { get; set; }
        //
        public ICollection<Order> Orders { get; set; }
    }
}
