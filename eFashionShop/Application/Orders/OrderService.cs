using eFashionShop.Data.EF;
using eFashionShop.Data.Entities;
using eFashionShop.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eFashionShop.Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly EShopDbContext _context;

        public OrderService(EShopDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) throw new EShopException("Delete fail!");
            _context.Orders.Remove(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Order>> GetAll(string keyword)
        {
            var res = from order in _context.Orders where order.ShipName == keyword select order;
            return await res.ToListAsync();
        }

        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<bool> Update(Order order)
        {
            if (order == null) throw new EShopException("Update fail!");
            _context.Orders.Update(order);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
