using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence;

public class DevFreelaDbContext
{
    public List<Project> Projects { get; set; }
    public List<User> Users { get; set; }
    public List<Skill> Skills { get; set; }

    public DevFreelaDbContext()
    {
        Projects = [new Project("Test P1", "Test P1", 1, 1, 10000),
                    new Project("Test P2", "Test P2", 1, 1, 20000),
                    new Project("Test P3", "Test P3", 1, 1, 30000)];

        Users = [new User("User 1", "E-mail 1", new DateTime(1997, 12, 31)),
                 new User("User 2", "E-mail 2", new DateTime(1990, 10, 25))];

        Skills = [new Skill("Skill 1"), new Skill("Skill 2")];
    }
}
