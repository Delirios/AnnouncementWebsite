using System;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
        }
    }
}
