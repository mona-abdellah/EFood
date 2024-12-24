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
    public class OrderItemRepository:GenericRepository<OrderItem,Guid>,IOrderItemRepository
    {
        private readonly FoodContext context;
        public OrderItemRepository(FoodContext _context) : base(_context)
        {
            context = _context;
        }
    }
}
