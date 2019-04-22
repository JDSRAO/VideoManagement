using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VideoManagement.Models;

namespace VideoManagement.DataAccess.SQLite.Entities
{
    public class CategoriesMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name);

            builder.ToTable("Categories");
        }
    }
}
