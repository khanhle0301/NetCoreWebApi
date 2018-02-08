using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoolBaby.Data.Enums;
using CoolBaby.Data.Interfaces;
using CoolBaby.Infrastructure.SharedKernel;

namespace CoolBaby.Data.Entities
{
    [Table("Blogs")]
    public class Blog : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        public Blog() {}

        public Blog(string name, string thumbnailImage,
           string description, string content, bool hotFlag,
           string tags, Status status, string seoPageTitle,
           string seoAlias, string seoMetaKeyword,
           string seoMetaDescription, int categoryId)
        {
            Name = name;
            Image = thumbnailImage;
            Description = description;
            Content = content;
            Tags = tags;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
            CategoryId = categoryId;
            HotFlag = hotFlag;
        }

        public Blog(int id, string name, string thumbnailImage, string description,
            string content, bool hotFlag, string tags, Status status, string seoPageTitle,
            string seoAlias, string seoMetaKeyword, string seoMetaDescription, int categoryId)
        {
            Id = id;
            Name = name;
            Image = thumbnailImage;
            Description = description;
            Content = content;
            CategoryId = categoryId;
            Tags = tags;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
            HotFlag = hotFlag;
        }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }

        [DefaultValue(0)]
        public int ViewCount { set; get; }

        public string Tags { get; set; }

        public virtual ICollection<BlogTag> BlogTags { set; get; }

        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public Status Status { set; get; }

        [MaxLength(256)]
        public string SeoPageTitle { set; get; }

        [MaxLength(256)]
        public string SeoAlias { set; get; }

        [MaxLength(256)]
        public string SeoKeywords { set; get; }

        [MaxLength(256)]
        public string SeoDescription { set; get; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual BlogCategory BlogCategory { set; get; }

        public bool HotFlag { set; get; }
    }
}