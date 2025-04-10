using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using DevFreela.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence;

public class DevFreelaDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options)
    {

    }

    // Tabelas
    public DbSet<Project> Projects { get; set; }
    public new DbSet<User> Users { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }
    public DbSet<ProjectComment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica configurações do Identity primeiro
        base.OnModelCreating(modelBuilder);
        
        // Adiciona configurações do IEntityTypeConfiguration através do assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
