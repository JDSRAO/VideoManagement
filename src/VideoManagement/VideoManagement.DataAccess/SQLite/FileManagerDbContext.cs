﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VideoManagement.Models.Tables;

namespace VideoManagement.DataAccess.SQLite
{
    public class FileManagerDbContext : DbContext
    {
        public string ConnectionString { get; }

        public DbSet<AppFile> Files { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Setting> Settings { get; set; }


        public FileManagerDbContext(string connectionString)
        {
            ConnectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={ConnectionString}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Setting>().HasData(
                new Setting
                {
                    ID = Guid.NewGuid(),
                    Key = "DbExists",
                    Value = "false"
                });
        }
    }
}
