using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VideoManagement.Models.Application;

namespace VideoManagement.DataAccess.SQLite.Application.Configuration
{
    public class RecentPathsMap : IEntityTypeConfiguration<RecentPath>
    {
        public void Configure(EntityTypeBuilder<RecentPath> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x => x.Path);
            builder.Property(x => x.Extension);
            builder.Property(x => x.AccessedOn);

            builder.HasIndex(x => x.Path);

            builder.ToTable("RecentPaths");
        }
    }
}
