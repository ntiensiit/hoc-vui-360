using HocVui360.Entity;
using HocVui360.Entity.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HocVui360.Lib.Data;

public class IdentityAppDbContext : IdentityDbContext<
    IdentityUserApp,
    IdentityRoleApp,
    string,
    IdentityUserClaimApp,
    IdentityUserRoleApp,
    IdentityUserLoginApp,
    IdentityRoleClaimApp,
    IdentityUserTokenApp>
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<FeaturedCourse> FeaturedCourses { get; set; }
    public DbSet<OpeningSchedule> OpeningSchedules { get; set; }
    public DbSet<TrainingProgram> TrainingPrograms { get; set; }
}
