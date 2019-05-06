using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VideoManagement.Models.Tables;

namespace VideoManagement.DataAccess.SQLite.Entities
{
    class ArtistsMap : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name);

            builder.ToTable("Artists");
        }
    }
}
