using Microsoft.EntityFrameworkCore;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VinEcomDomain.Model
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        //
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Store> Stores { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
