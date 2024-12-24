using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTO.Order
{
    public class GetAllOrderItemDTO
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? OrderId { get; set; }
    }
}
