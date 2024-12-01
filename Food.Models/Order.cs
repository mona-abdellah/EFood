using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Models
{
    public class Order:BaseEntity<Guid>
    {
        public DateTime OrderDate { get; set; }
        [Column(TypeName="money")]
        public int TotalPrice { get; set; }

        [Range(0, 6)]
        public int Status { get; set; } = 0;
        [ForeignKey("Customer")]
        public Guid? customerId { get; set; }
        public Customer? Customer { get; set; }
        [ForeignKey("Payment")]
        public Guid? PaymentId { get; set; }
        public Payment? payment { get; set; }
        [ForeignKey("Shipment")]
        public Guid? shipmentId { get; set; }
        public Shipment? shipment { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }

    }
}
