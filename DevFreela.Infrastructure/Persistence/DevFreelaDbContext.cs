using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace DevFreela.Infrastructure.Persistence;

public class DevFreelaDbContext : DbContext
{
    public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options)
    {

    }

    // Tabelas
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }
    public DbSet<ProjectComment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Adiciona configurações do IEntityTypeConfiguration através do assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
