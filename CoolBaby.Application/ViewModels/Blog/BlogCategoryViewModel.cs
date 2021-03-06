﻿using System;
using System.Collections.Generic;
using CoolBaby.Data.Enums;

namespace CoolBaby.Application.ViewModels.Blog
{
    public class BlogCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public int SortOrder { set; get; }

        public Status Status { set; get; }

        public string SeoPageTitle { set; get; }

        public string SeoAlias { set; get; }

        public string SeoKeywords { set; get; }

        public string SeoDescription { set; get; }

        public ICollection<BlogViewModel> Blogs { set; get; }
    }
}