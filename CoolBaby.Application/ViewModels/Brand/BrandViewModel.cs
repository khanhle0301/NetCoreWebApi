using System;
using System.Collections.Generic;
using CoolBaby.Application.ViewModels.Product;
using CoolBaby.Data.Enums;

namespace CoolBaby.Application.ViewModels.Brand
{
    public class BrandViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public int SortOrder { set; get; }

        public Status Status { set; get; }

        public string SeoPageTitle { set; get; }

        public string SeoAlias { set; get; }

        public string SeoKeywords { set; get; }

        public string SeoDescription { set; get; }

        public ICollection<ProductViewModel> Products { set; get; }
    }
}