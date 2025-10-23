using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = HocVui360.Entity.File;

namespace HocVui360.Lib.Data.EntityConfig;

internal class FileConfig : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("Files");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name);

        builder.Property(x => x.ContentType);

        builder.Property(x => x.Data);

        builder.Property(x => x.Size);

        builder.Property(x => x.CreatedAt);

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.RowVersion);

        builder.Property(x => x.IsDeleted);
    }
}
