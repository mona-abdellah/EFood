using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Models
{
    public class Payment:BaseEntity<Guid>
    {
        public DateTime PaymentDate { get; set; }
        public string Methode { get; set; }
        public decimal? Amount { get; set; }
        public Customer? Customer { get; set; }
        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }
        public ICollection<Order>? Orders { get; set; }

    }
}
