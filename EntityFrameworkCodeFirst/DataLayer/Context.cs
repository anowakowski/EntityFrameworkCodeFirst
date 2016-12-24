﻿using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Context : DbContext
    {
        public Context()
            : base()
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Alias_DataAnnotationsExample> Aliases { get; set; }
        public DbSet<Tweet_DataAnnotationsExample> Tweets { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Alias_FluentConfig> Aliases_FluentConfig { get; set; }
        public DbSet<Tweet_FluentConfig> Tweet_FluentConfig { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Blog>().HasKey(m => m.Id);
            //modelBuilder.Entity<Blog>().Property(m => m.Title).HasMaxLength(20);
            //modelBuilder.Entity<Blog>().Property(m => m.BloggerName).IsRequired();

            //modelBuilder.Entity<Post>().HasKey(m => m.Id);
            //modelBuilder.Entity<Post>().Property(m => m.Content).HasMaxLength(100);
            //modelBuilder.Entity<Post>().Property(m => m.Title).HasMaxLength(20);

            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Alias_FluentConfig>().ToTable("Alias_FluentConfiguration", "FulentConfig");
            modelBuilder.Entity<Alias_FluentConfig>().HasKey(p => p.AliasKey);
            modelBuilder.Entity<Alias_FluentConfig>()
                .Property(p => p.CreateDate)
                .HasColumnName("StartDate")
                //.HasColumnOrder(3)
                .HasColumnType("date")
                .IsRequired();
            modelBuilder.Entity<Alias_FluentConfig>()
                .Property(p => p.Name)
                .IsFixedLength() //in db is nchar type
                .IsMaxLength();
            modelBuilder.ComplexType<Privacy_FluentConfig>().Property(p => p.Test).HasColumnName("TestingPrivacy");
            modelBuilder.Ignore<PrivacyToIgnore_FluentConfig>();

            //-----------------------------------------------------------------------------------------------------------------------------
            //if you would like to use bottom configu you should comment code above. Because you mapping entity Alias_FluentConfig not once!
            //-----------------------------------------------------------------------------------------------------------------------------
            modelBuilder.Entity<Alias_FluentConfig>()
                .Map(mapping =>
                {
                    mapping.Properties(p => new { p.AliasKey, p.UserName });
                    mapping.ToTable("AliasFirstTable");
                    
                })
                .Map(mapping =>
                {
                    mapping.Properties(p => new { p.AliasKey, p.Email });
                    mapping.ToTable("AliasSecondTable");
                });
            modelBuilder.Ignore<PrivacyToIgnore_FluentConfig>();

        }
    }
}
