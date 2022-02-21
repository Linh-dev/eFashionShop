using eFashionShop.Data.EF;
using eFashionShop.ViewModels.Catalog.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eFashionShop.Exceptions;
using eFashionShop.Data.Entities;
using eFashionShop.Data.Enums;

namespace eFashionShop.Application.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly EShopDbContext _context;

        public CategoryService(EShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(CategoryCreateVm categoryVm)
        {
            if (categoryVm == null) throw new EShopException("Create fail!");
            var category = new Category
            {
                Name = categoryVm.Name,
                SeoDescription = categoryVm.SeoDescription,
                SeoTitle = categoryVm.SeoTitle,
                SeoAlias = categoryVm.SeoAlias,
                SortOrder = categoryVm.SortOrder,
                IsShowOnHome = categoryVm.IsShowOnHome,
                ParentId = categoryVm.ParentId,
                Status = categoryVm.Status
            };
            _context.Categories.Add(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) throw new EShopException("Delete fail!");
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<CategoryVm>> GetAll()
        {
            var query = from c in _context.Categories
                        select new { c };
            return await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                ParentId = x.c.ParentId
            }).ToListAsync();
        }

        public async Task<CategoryVm> GetById(int id)
        {
            var query = from c in _context.Categories where c.Id == id  
                        select new { c };
            return await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                ParentId = x.c.ParentId
            }).FirstOrDefaultAsync();
        }

        public async Task<List<CategoryVm>> GetListParent()
        {
            var query = from c in _context.Categories where c.ParentId == null && c.Status == Status.Active
                        select new { c };
            var x = await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                ParentId = x.c.ParentId
            }).ToListAsync();
            return x;
        }
    }
}
