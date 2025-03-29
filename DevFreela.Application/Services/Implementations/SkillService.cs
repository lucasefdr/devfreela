using DevFreela.Application.Common;
using DevFreela.Application.DTOs.InputModels.Skill;
using DevFreela.Application.DTOs.ViewModels.Skill;
using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Application.Repositories;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Services.Implementations;

public class SkillService(ISkillRepository skillRepository) : ISkillService
{
    #region CREATE
    public async Task<int> Create(SkillInputModel model)
    {
        var newSkill = new Skill(model.Description);
        await skillRepository.CreateAsync(newSkill);
        await skillRepository.CommitAsync();

        return newSkill.Id;
    }
    #endregion

    #region READ
    public async Task<PagedResult<SkillViewModel>> GetAll(QueryParameters parameters)
    {
        var skillsPaged = await skillRepository.GetAllAsync(parameters);

        var skillsPagedResponse = new PagedResult<SkillViewModel>()
        {
            CurrentPage = skillsPaged.CurrentPage,
            PageSize = skillsPaged.PageSize,
            TotalCount = skillsPaged.TotalCount,
            TotalPages = skillsPaged.TotalPages,
            Items = [.. skillsPaged.Items.Select(s => new SkillViewModel(s.Id, s.Description))]
        };

        return skillsPagedResponse;
    }

    public async Task<SkillViewModel?> GetById(int id)
    {
        var skill = await skillRepository.FindAsync(id);

        if (skill == null) return null;

        return new SkillViewModel(skill.Id, skill.Description);
    }
    #endregion
}
