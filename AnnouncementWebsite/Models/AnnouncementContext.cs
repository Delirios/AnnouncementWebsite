﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementWebsite.Models
{
    public class AnnouncementContext : DbContext
    {
        public AnnouncementContext(DbContextOptions<AnnouncementContext> options) : base(options)
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<AnnouncementImage> AnnouncementImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AnnouncementImage>().HasKey(a => new {a.AnnouncementId, a.ImageId});

            modelBuilder.Entity<AnnouncementImage>()
                .HasOne(ai => ai.Announcement)
                .WithMany(a => a.AnnouncementImages)
                .HasForeignKey(ai => ai.AnnouncementId);

            modelBuilder.Entity<AnnouncementImage>()
                .HasOne(ai => ai.Image)
                .WithMany(a => a.AnnouncementImages)
                .HasForeignKey(ai => ai.ImageId);


            modelBuilder.Entity<Category>().HasData(new Category {CategoryId = 1, CategoryName = "Vehicle"});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId = 2, CategoryName = "Others"});

            modelBuilder.Entity<Announcement>().HasData(new Announcement
            {
                AnnouncementId = 1,
                Title = "First",
                Description = "Some First Description",
                DateAdded = DateTime.Now,
                CategoryId = 1
            });
            modelBuilder.Entity<Announcement>().HasData(new Announcement
            {
                AnnouncementId = 2,
                Title = "Second",
                Description = "Some Second Description",
                DateAdded = DateTime.Now,
                CategoryId = 1
            });
            modelBuilder.Entity<Announcement>().HasData(new Announcement
            {
                AnnouncementId = 3,
                Title = "Third",
                Description = "Some Third Description",
                DateAdded = DateTime.Now,
                CategoryId = 2
            });
            modelBuilder.Entity<Image>().HasData(new Image
            {
                ImageId = 1,
                Name = "1.jpg"
            });
            modelBuilder.Entity<Image>().HasData(new Image
            {
                ImageId = 2,
                Name = "2.jpg"
            });

            modelBuilder.Entity<AnnouncementImage>().HasData(new AnnouncementImage
            {
                AnnouncementImageId = 1,
                AnnouncementId = 1,
                ImageId = 1
            });
            modelBuilder.Entity<AnnouncementImage>().HasData(new AnnouncementImage
            {
                AnnouncementImageId = 2,
                AnnouncementId = 2,
                ImageId = 2
            });
            modelBuilder.Entity<AnnouncementImage>().HasData(new AnnouncementImage
            {
                AnnouncementImageId = 3,
                AnnouncementId = 3,
                ImageId = 1
            });
        }
    }
}