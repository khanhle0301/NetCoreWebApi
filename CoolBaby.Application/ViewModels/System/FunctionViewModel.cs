﻿using CoolBaby.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoolBaby.Application.ViewModels.System
{
    public class FunctionViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { set; get; }

        [Required]
        [StringLength(250)]
        public string URL { set; get; }

        [StringLength(128)]
        public string ParentId { set; get; }

        public string IconCss { get; set; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }

        public ICollection<FunctionViewModel> ChildFunctions { set; get; }
    }
}