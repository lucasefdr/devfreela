using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations;

internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(e => e.ID);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(250);   

        builder.Property(p => p.TotalCost)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
       
        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.StartedAt);

        builder.Property(p => p.FinishedAt);

        builder.Property(p => p.CancelledAt);

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(p => p.Client)
            .WithMany(c => c.OwnedProjects)
            .HasForeignKey(p => p.ClientID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Freelancer)
            .WithMany(f => f.FreelancerProjects)
            .HasForeignKey(p => p.FreelancerID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
