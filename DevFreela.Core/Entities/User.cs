using DevFreela.Core.Common;
using DevFreela.Core.Enums;
using System.Text.Json.Serialization;

namespace DevFreela.Core.Entities;

public class User : BaseEntity
{
    #region Construtores

    public User(string fullName, string email, DateTime birthDate, UserTypeEnum userType, string password, string role)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        IsActive = true;
        CreatedAt = DateTime.Now;
        UserType = userType;
        Password = password;
        Role = role;

        UserSkills = [];
        OwnedProjects = [];
        FreelancerProjects = [];
        Comments = [];
    }

    #endregion

    #region Propriedades

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }
    public UserTypeEnum UserType { get; private set; }
    
    // Propriedades para autentição e autorização
    public string Password { get; private set; }
    public string Role { get; private set; }

    #endregion

    #region Propriedades de navegação

    public List<UserSkill> UserSkills { get; private set; }
    public List<Project> OwnedProjects { get; private set; }
    public List<Project> FreelancerProjects { get; private set; }
    public List<ProjectComment> Comments { get; private set; }

    #endregion

    #region Métodos

    public Result AddSkill(Skill skill)
    {
        if (UserSkills.Any(s => s.SkillId == skill.Id))
            return Result.Failure("Skill already added.");

        UserSkills.Add(new UserSkill(Id, skill.Id));
        return Result.Success();
    }

    public Result ActiveUser()
    {
        if (IsActive) return Result.Failure("User is already active.");

        IsActive = true;
        return Result.Success();
    }


    public Result InactiveUser()
    {
        if (!IsActive) return Result.Failure("User is not active.");

        IsActive = false;
        return Result.Success();
    }

    #endregion
}