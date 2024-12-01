using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Models
{
    public  class Favorite:BaseEntity<Guid>
    {
        [ForeignKey("Customer")]
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        [ForeignKey("Product")]
        public Guid? ProductId { get;set;}
        public Product? Product { get; set; }
    }
}
