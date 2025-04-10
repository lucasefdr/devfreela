using DevFreela.Core.Common;
using DevFreela.Core.Enums;
using System.Text.Json.Serialization;

namespace DevFreela.Core.Entities;

public class User : BaseEntity
{
    #region Construtores

    public User(string fullName, DateTime birthDate, UserTypeEnum userType)
    {
        FullName = fullName;
        BirthDate = birthDate;
        IsActive = true;
        UserType = userType;

        UserSkills = [];
        OwnedProjects = [];
        FreelancerProjects = [];
        Comments = [];
    }

    public User(int id, string fullName, DateTime birthDate, UserTypeEnum userType)
    {
        Id = id;
        FullName = fullName;
        BirthDate = birthDate;
        IsActive = true;
        UserType = userType;

        UserSkills = [];
        OwnedProjects = [];
        FreelancerProjects = [];
        Comments = [];
    }

    #endregion

    #region Propriedades

    public string FullName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool IsActive { get; private set; }
    public UserTypeEnum UserType { get; private set; }

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
            return Result.Failure(Error.Validation("User.AddSkill", "This skill already exists."));

        UserSkills.Add(new UserSkill(Id, skill.Id));
        return Result.Success();
    }

    public Result ActiveUser()
    {
        if (IsActive) return Result.Failure(Error.Validation("User.Active", "This user already exists."));

        IsActive = true;
        return Result.Success();
    }


    public Result InactiveUser()
    {
        if (!IsActive) return Result.Failure(Error.Validation("User.Inactive", "This user is not active."));

        IsActive = false;
        return Result.Success();
    }

    #endregion
}