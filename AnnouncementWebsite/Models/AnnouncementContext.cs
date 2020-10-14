using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementWebsite.Models
{
    public class AnnouncementContext : IdentityDbContext<AplicationUser>
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


            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, CategoryName = "Vehicle" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Garden" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Electronics" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 4, CategoryName = "Fashion" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 5, CategoryName = "Sports" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 6, CategoryName = "Others" });
        }
    }

    public class AplicationUser : IdentityUser
    {
        public List<Announcement> Announcements { get; set; }
    }
}
