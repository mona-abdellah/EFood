using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Models
{
    public class Customer:IdentityUser<Guid>
    {
        [MinLength(3)]
        public string FName { get; set; }
        [MinLength(3)]
        public string Lname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Shipment>? Shipments { get; set; }
    }
}
