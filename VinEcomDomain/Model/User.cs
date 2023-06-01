using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinEcomDomain.Model
{
#pragma warning disable CS8618
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public string? Email { get; set; }
        public bool IsBlocked { get; set; }
        public string? AvatarUrl { get; set; }
        //
        public Customer Customer { get; set; }
        public StoreStaff Staff { get; set; }
        public Shipper Shipper { get; set; }
    }
}
