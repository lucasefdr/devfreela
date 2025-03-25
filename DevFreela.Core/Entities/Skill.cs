using DevFreela.Core.Common;

namespace DevFreela.Core.Entities;

public class Skill : BaseEntity
{
    #region Construtores
    public Skill(string description)
    {
        Description = description;
        CreatedAt = DateTime.Now;

        UserSkills = [];
    }
    #endregion

    #region Propriedades
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    #endregion

    #region Propriedades de navegação
    public List<UserSkill> UserSkills { get; private set; }
    #endregion

}
