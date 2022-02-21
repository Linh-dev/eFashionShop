using eFashionShop.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eFashionShop.Application.Orders
{
    public interface IOrderService
    {
        Task<List<Order>> GetAll(string keyword);
        Task<bool> Delete(int id);
        Task<bool> Update(Order order);
        Task<Order> GetById(int id);
    }
}
