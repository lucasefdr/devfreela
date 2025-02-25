using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations;

public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        builder.HasKey(us => new { us.IdUser, us.IdSkill });
        builder.HasOne(us => us.User).WithMany(u => u.UserSkills).HasForeignKey(us => us.IdUser);
        builder.HasOne(us => us.Skill).WithMany(s => s.UserSkills).HasForeignKey(us => us.IdSkill);
    }
}
