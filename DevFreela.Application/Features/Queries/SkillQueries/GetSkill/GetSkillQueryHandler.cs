using DevFreela.Application.DTOs.ViewModels.Skill;
using DevFreela.Application.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Queries.SkillQueries.GetSkill;

public class GetSkillQueryHandler(ISkillRepository skillRepository) : IRequestHandler<GetSkillQuery, SkillViewModel?>
{
    private readonly ISkillRepository _skillRepository = skillRepository;
    public async Task<SkillViewModel?> Handle(GetSkillQuery request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.FindAsync(request.Id);

        return skill is not null ? new SkillViewModel(skill.Id, skill.Description) : null;
    }
}
