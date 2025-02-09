using DevFreela.Application.ViewModels.Skill;

namespace DevFreela.Application.Services.Interfaces;

public interface ISkillService
{
    List<SkillViewModel> GetAll();
}
