﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CoolBaby.Infrastructure.SharedKernel;

namespace CoolBaby.Data.Entities
{
    [Table("Footers")]
    public class Footer : DomainEntity<string>
    {

        [Required]
        public string Content { set; get; }
    }
}
