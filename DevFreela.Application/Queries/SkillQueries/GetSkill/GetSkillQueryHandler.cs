using DevFreela.Application.ViewModels.Skill;
using DevFreela.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.SkillQueries.GetSkill;

public class GetSkillQueryHandler(ISkillRepository skillRepository) : IRequestHandler<GetSkillQuery, SkillViewModel?>
{
    private readonly ISkillRepository _skillRepository = skillRepository;
    public async Task<SkillViewModel?> Handle(GetSkillQuery request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.Get()
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        return skill is not null ? new SkillViewModel(skill.Id, skill.Description) : null;
    }
}
