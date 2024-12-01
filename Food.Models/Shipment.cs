using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Models
{
    public class Shipment:BaseEntity<Guid>
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string ZipCode { get; set; }
        public Customer? Customer { get; set; }
        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }
        public ICollection<Order>? orders { get; set; }
    }
}
