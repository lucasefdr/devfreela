using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.Skill;
using DevFreela.Application.DTOs.ViewModels.Skill;

namespace DevFreela.Application.Services.Interfaces;

public interface ISkillService
{
    Task<PagedResult<SkillViewModel>> GetAll(QueryParameters parameters);
    Task<SkillViewModel?> GetByID(int id);
    Task<int> Create(SkillInputModel model);
}
