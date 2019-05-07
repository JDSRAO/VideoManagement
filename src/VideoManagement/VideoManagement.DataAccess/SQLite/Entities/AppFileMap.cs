using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VideoManagement.Models.Tables;

namespace VideoManagement.DataAccess.SQLite.Entities
{
    public class AppFileMap : IEntityTypeConfiguration<AppFile>
    {
        public void Configure(EntityTypeBuilder<AppFile> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x => x.Name);
            builder.Property(x => x.Path);
            builder.Property(x => x.Categories);
            builder.Property(x => x.Tags);
            builder.Property(x => x.CreatedOn);
            builder.Property(x => x.LastAccessTime);
            builder.Property(x => x.Views);
            builder.Property(x => x.Artists);
            builder.Property(x => x.Favourite);

            builder.HasIndex(x => x.Path);
            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.Categories);
            builder.HasIndex(x => x.Tags);

            builder.ToTable("Files");
        }
    }
}
