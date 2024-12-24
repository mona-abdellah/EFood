using Food.Apllication.Contracts;
using Food.Context;
using Food.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Infrastructure
{
    public class ShipmentRepository:GenericRepository<Shipment,Guid>,IShipmentRepository
    {
        private readonly FoodContext context;
        public ShipmentRepository(FoodContext _context) : base(_context)
        {
            context = _context;
        }
    }
}
