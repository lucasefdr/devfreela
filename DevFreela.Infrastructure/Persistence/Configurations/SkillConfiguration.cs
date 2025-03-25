using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(s => s.ID);

        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(s => s.CreatedAt)
            .IsRequired();
    }
}
