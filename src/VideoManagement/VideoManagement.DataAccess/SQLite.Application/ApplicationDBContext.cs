﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VideoManagement.Models.Tables;

namespace VideoManagement.DataAccess.SQLite.Application
{
    public class ApplicationDBContext : DbContext
    {
        public string ConnectionString { get; }

        public DbSet<RecentPath> RecentPaths { get; set; }
        public DbSet<FileExtensions> FileExtensions { get; set; }

        public ApplicationDBContext()
        {
            ConnectionString = Path.Combine(Directory.GetCurrentDirectory(),"db", "appData.db");
            if(!File.Exists(ConnectionString))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ConnectionString));
                File.Create(ConnectionString);
            }
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={ConnectionString}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FileExtensions>().HasData(Models.Tables.FileExtensions.DefaultExtensions());
        }
    }
}
