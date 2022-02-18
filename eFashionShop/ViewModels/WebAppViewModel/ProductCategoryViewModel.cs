using eFashionShop.ViewModels.Catalog.Categories;
using eFashionShop.ViewModels.Catalog.Products;
using eFashionShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eFashionShop.ViewModels.WebAppViewModel
{
    public class ProductCategoryViewModel
    {
        public CategoryVm Category { get; set; }

        public PagedResult<ProductVm> Products { get; set; }
    }
}