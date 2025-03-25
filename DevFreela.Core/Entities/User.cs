using DevFreela.Core.Common;
using DevFreela.Core.Enums;
using System.Text.Json.Serialization;

namespace DevFreela.Core.Entities;

public class User : BaseEntity
{
    #region Construtores
    public User(string fullName, string email, DateTime birthDate, UserTypeEnum userType)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        IsActive = true;
        CreatedAt = DateTime.Now;
        UserType = userType;

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
    #endregion

    #region Propriedades de navegação
    public List<UserSkill> UserSkills { get; private set; }
    public List<Project> OwnedProjects { get; private set; }
    public List<Project> FreelancerProjects { get; private set; }
    public List<ProjectComment> Comments { get; private set; }
    #endregion

    #region Métodos
    public void AddSkill(Skill skill)
    {
        if (UserSkills.Any(s => s.IdSkill == skill.ID))
            throw new InvalidOperationException("Skill already added.");

        UserSkills.Add(new UserSkill(ID, skill.ID));
    }

    public void ActiveUser()
    {
        if (!IsActive)
            IsActive = true;
    }

    public void InactiveUser()
    {
        if (IsActive)
            IsActive = false;
    }
    #endregion
}
