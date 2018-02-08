using CoolBaby.Data.Enums;
using CoolBaby.Data.Interfaces;
using CoolBaby.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBaby.Data.Entities
{
    [Table("Advertistments")]
    public class Advertistment : DomainEntity<int>, ISwitchable, ISortable
    {
        public Advertistment()
        {
        }

        public Advertistment(string name, string description, string image, string url,
            string positionId, Status status, int sortOrder)
        {
            Name = name;
            Description = description;
            Image = image;
            Url = url;
            PositionId = positionId;
            SortOrder = sortOrder;
            Status = status;
        }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        [StringLength(20)]
        public string PositionId { get; set; }

        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int SortOrder { set; get; }

        [ForeignKey("PositionId")]
        public virtual AdvertistmentPosition AdvertistmentPosition { get; set; }
    }
}