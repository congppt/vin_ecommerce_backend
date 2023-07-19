using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinEcomViewModel.Base
{
    public class AuthorizedViewModel
    {
        public string AccessToken { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public string? LicensePlate { get; set; }
    }
}
