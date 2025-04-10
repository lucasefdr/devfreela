using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.BirthDate)
            .IsRequired();

        builder.Property(u => u.IsActive)
            .IsRequired();

        builder.Property(u => u.UserType)
            .IsRequired()
            .HasConversion<string>();
        
        builder.HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<User>(u => u.Id) // FK para AspNetUsers (Id)
            .OnDelete(DeleteBehavior.Cascade); // Se ApplicationUser for excluído, User também vai
    }
}
