using HocVui360.Entity;
using HocVui360.Entity.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using File = HocVui360.Entity.File;

namespace HocVui360.Lib.Data;

public class IdentityAppDbContext : IdentityDbContext<IdentityUserApp, IdentityRoleApp, string>
{
    public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<FeaturedCourse> FeaturedCourses { get; set; }
    public DbSet<OpeningSchedule> OpeningSchedules { get; set; }
    public DbSet<TrainingProgram> TrainingPrograms { get; set; }
    public DbSet<File> Files { get; set; }
}
