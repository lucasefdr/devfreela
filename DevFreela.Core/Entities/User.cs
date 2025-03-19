using System.Text.Json.Serialization;

namespace DevFreela.Core.Entities;

public class User : BaseEntity
{
    public User(string fullName, string email, DateTime birthDate)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        IsActive = true;
        CreatedAt = DateTime.Now;

        UserSkills = [];
        OwnedProjects = [];
        FreelanceProjects = [];
        Comments = [];
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; set; }

    public List<UserSkill> UserSkills { get; private set; }
    public List<Project> OwnedProjects { get; private set; }
    public List<Project> FreelanceProjects { get; private set; }
    public List<ProjectComment> Comments { get; private set; }

    public void AddSkill(Skill skill)
    {
        if (UserSkills.Any(s => s.IdSkill == skill.Id))
            throw new InvalidOperationException("Skill already added.");

        UserSkills.Add(new UserSkill(Id, skill.Id));
    }
}
