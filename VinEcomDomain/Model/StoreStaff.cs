using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinEcomDomain.Model
{
    public class StoreStaff
    {
        public int Id { get; set; }
        //
        public int UserId { get; set; }
        public User User { get; set; }
        //
        public int StoreId { get; set; }
        public Store Store { get; set; } 
    }
}
