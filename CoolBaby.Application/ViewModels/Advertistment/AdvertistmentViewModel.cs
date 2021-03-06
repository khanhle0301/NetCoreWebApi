﻿using System;
using System.ComponentModel.DataAnnotations;
using CoolBaby.Data.Enums;

namespace CoolBaby.Application.ViewModels.Advertistment
{
    public class AdvertistmentViewModel
    {
        public int Id { get; set; }

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

        public AdvertistmentPositionViewModel AdvertistmentPosition { get; set; }
    }
}