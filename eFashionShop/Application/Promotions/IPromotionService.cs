using eFashionShop.Data.Entities;
using eFashionShop.ViewModels.Catalog.Promotions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eFashionShop.Application.Promotions
{
    public interface IPromotionService
    {
        Task<List<Promotion>> GetAll();
        Task<bool> Delete(int id);
        Task<bool> Create(PromotionCreateVm res);
    }
}
