using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTO.Order
{
    public class GetAllOrderDTO
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalPrice { get; set; }
        public int Status { get; set; } = 0;
        public Guid? customerId { get; set; }
        public Guid? PaymentId { get; set; }
        public Guid? shipmentId { get; set; }
        public ICollection<CreateORUpdateOrderItemDTO> OrderItems { get; set; }
    }
}
