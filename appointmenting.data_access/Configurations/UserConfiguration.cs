using appointmenting.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appointmenting.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(x => x.Id).HasName("pk_user");

        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()").HasColumnName("id");
    }
}
