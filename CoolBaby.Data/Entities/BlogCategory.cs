using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoolBaby.Data.Enums;
using CoolBaby.Data.Interfaces;
using CoolBaby.Infrastructure.SharedKernel;

namespace CoolBaby.Data.Entities
{
    [Table("BlogCategories")]
    public class BlogCategory : DomainEntity<int>,
        IHasSeoMetaData, ISwitchable, ISortable, IDateTracking
    {
        public BlogCategory()
        {
            Blogs = new List<Blog>();
        }

        public BlogCategory(string name, int sortOrder, Status status, string seoPageTitle,
            string seoAlias, string seoKeywords, string seoDescription)
        {
            Name = name;
            SortOrder = sortOrder;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeywords;
            SeoDescription = seoDescription;
        }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public int SortOrder { set; get; }

        public Status Status { set; get; }

        [StringLength(255)]
        public string SeoPageTitle { set; get; }

        [Required]
        [StringLength(255)]
        public string SeoAlias { set; get; }

        [StringLength(255)]
        public string SeoKeywords { set; get; }

        [StringLength(255)]
        public string SeoDescription { set; get; }

        public virtual ICollection<Blog> Blogs { set; get; }
    }
}