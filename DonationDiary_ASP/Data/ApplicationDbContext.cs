using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DonationDiary_ASP.Models;
using Microsoft.AspNetCore.Identity;

namespace DonationDiary_ASP.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Entry> Entry { get; set; }
        public DbSet<user_entries> User_entries { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<User_Details> User_Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(x => new
            {
                x.LoginProvider,
                x.ProviderKey
            });

            modelBuilder.Entity<user_entries>().HasKey(a => new {a.UserId, a.EntryId});

            modelBuilder.Entity<user_entries>().HasOne(a => a.User)
                .WithMany(b => b.user_Entries)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<user_entries>().HasOne(a => a.Entry)
                .WithMany(b => b.user_Entries)
                .HasForeignKey(a => a.EntryId);

            modelBuilder.Entity<User>().HasOne(a => a.UserDetails)
                .WithOne(b => b.User)
                .HasForeignKey<User_Details>(b => b.UserId);

        }
    }
}