using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoolBaby.Data.EF.Extensions;
using CoolBaby.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoolBaby.Data.EF.Configurations
{
    public class AnnouncementConfiguration : DbEntityConfiguration<Announcement>
    {
        public override void Configure(EntityTypeBuilder<Announcement> entity)
        {
            entity.Property(c => c.Id)
                .IsRequired().HasColumnType("varchar(128)");
        }
    }
}
