using AngularAuthYtAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthYtAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<applicant> Applicant { get; set; }
        public DbSet<jobs> job { get; set; }
        public DbSet<Resumes> Resumes { get; set; }
        public DbSet<applied> appliedjobs { get; set; }


        public DbSet<Applied> appliedjob { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("users");
            builder.Entity<applicant>().ToTable("Applicant");
            builder.Entity<jobs>().ToTable("job");
            builder.Entity<applied>().ToTable("appliedjobstable");
            builder.Entity<Resumes>().ToTable("Resumes");
            builder.Entity<Applied>().ToTable("appliedjob");
        }
    }
}
