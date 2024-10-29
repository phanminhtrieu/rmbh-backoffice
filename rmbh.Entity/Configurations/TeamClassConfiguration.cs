﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rmbh.Entity.Entities.Manipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rmbh.Entity.Configurations
{
    public class TeamClassConfiguration : IEntityTypeConfiguration<TeamClass>
    {
        public void Configure(EntityTypeBuilder<TeamClass> builder)
        {
            builder.ToTable("TeamClasses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn().IsRequired();
            builder.HasOne(x => x.Team).WithMany(x => x.TeamClasses).HasForeignKey(x => x.TeamId);
            builder.HasOne(x => x.Class).WithMany(x => x.TeamClasses).HasForeignKey(x => x.ClassId);
        }
    }
}