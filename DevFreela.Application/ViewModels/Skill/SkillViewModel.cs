namespace DevFreela.Application.ViewModels.Skill;

public class SkillViewModel
{
    public int Id { get; private set; }
    public string Description { get; private set; }

    public SkillViewModel(int id, string description)
    {
        Id = id;
        Description = description;
    }
}
