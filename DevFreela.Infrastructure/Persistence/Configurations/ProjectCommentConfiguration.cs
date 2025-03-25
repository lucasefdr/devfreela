using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations;

public class ProjectCommentConfiguration : IEntityTypeConfiguration<ProjectComment>
{
    public void Configure(EntityTypeBuilder<ProjectComment> builder)
    {
        builder.HasKey(pc => pc.ID);

        builder.Property(pc => pc.Content)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(pc => pc.CreatedAt)
            .IsRequired();

        builder.HasOne(pc => pc.Project)
            .WithMany(p => p.Comments)
            .HasForeignKey(p => p.ProjectID);

        builder.HasOne(pc => pc.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(pc => pc.UserID);
    }
}
