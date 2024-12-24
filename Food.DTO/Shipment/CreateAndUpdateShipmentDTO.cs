using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DTO.Shipment
{
    public class CreateAndUpdateShipmentDTO
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string ZipCode { get; set; }
    }
}
