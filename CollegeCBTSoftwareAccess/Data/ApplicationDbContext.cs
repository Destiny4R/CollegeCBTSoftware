using CollegeCBTSoftwareModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CollegeCBTSoftwareAccess.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(450));

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(m => m.LoginProvider).HasMaxLength(127);
                entity.Property(m => m.ProviderKey).HasMaxLength(127);
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(127);
                entity.Property(m => m.RoleId).HasMaxLength(127);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(127);
                entity.Property(m => m.LoginProvider).HasMaxLength(127);
                entity.Property(m => m.Name).HasMaxLength(127);
            });
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<LevelRegistration> LevelRegistrations { get; set; }
        public DbSet<SessionTable> SessionTables { get; set; }
        public DbSet<ClassLeveTable> ClassLevelTables { get; set; }
        public DbSet<CourseTable> CourseTables { get; set; }
        public DbSet<StudentCourseReg> StudentCourseReg { get; set; }
        public DbSet<QuestionTable> QuestionTables { get; set; }
        public DbSet<StudentTable> StudentTables { get; set; }
        public DbSet<ExamReady> ExamReadies { get; set; }
    }
}