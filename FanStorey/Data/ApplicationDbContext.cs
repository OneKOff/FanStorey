using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FanStorey.Models;

namespace FanStorey.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Chapter> Chapter { get; set; }
        public DbSet<Story> User { get; set; }
        public DbSet<Fandom> Fandom { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<StoryRating> StoryRating { get; set; }
        public DbSet<ChapterRating> ChapterRating { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Preference> Preference { get; set; }
    }
}
