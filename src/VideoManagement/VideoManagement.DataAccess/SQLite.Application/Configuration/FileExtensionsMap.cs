using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VideoManagement.Models.Tables;

namespace VideoManagement.DataAccess.SQLite.Application.Configuration
{
    public class FileExtensionsMap : IEntityTypeConfiguration<FileExtensions>
    {
        public void Configure(EntityTypeBuilder<FileExtensions> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x => x.Format);
            builder.Property(x => x.Type);
            builder.Property(x => x.IsEnabled);

            builder.ToTable("FileExtensions");
        }
    }
}
