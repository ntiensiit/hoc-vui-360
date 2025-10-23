using HocVui360.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HocVui360.Lib.Data.EntityConfig;

internal class FeaturedCourseConfig : IEntityTypeConfiguration<FeaturedCourse>
{
    public void Configure(EntityTypeBuilder<FeaturedCourse> builder)
    {
        builder.ToTable("FeaturedCourses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt);

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.RowVersion);

        builder.Property(x => x.IsDeleted);
    }
}
