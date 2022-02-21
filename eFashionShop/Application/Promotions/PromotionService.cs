using eFashionShop.Data.EF;
using eFashionShop.Data.Entities;
using eFashionShop.Exceptions;
using eFashionShop.ViewModels.Catalog.Promotions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eFashionShop.Application.Promotions
{
    public class PromotionService : IPromotionService
    {
        private readonly EShopDbContext _context;

        public PromotionService(EShopDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(PromotionCreateVm res)
        {
            if (res == null) throw new EShopException("Create contact fail!");
            var promotion = new Promotion
            {
                Name = res.Name,
                FromDate = res.FromDate,
                ToDate = res.ToDate,
                ApplyForAll = res.ApplyForAll,
                DiscountPercent = res.DiscountPercent,
                DiscountAmount = res.DiscountAmount,
                ProductIds = res.ProductIds,
                ProductCategoryIds = res.ProductIds,
                Status = res.Status
            }; ;
            _context.Promotions.Add(promotion);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var promotion = _context.Promotions.Find(id);
            _context.Promotions.Remove(promotion);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Promotion>> GetAll()
        {
            var res = from p in _context.Promotions select p;
            return await res.ToListAsync();
        }
    }
}
