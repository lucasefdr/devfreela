using DevFreela.Application.ViewModels.Skill;
using DevFreela.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.SkillQueries.GetSkills;

public class GetSkillsQueryHandler(ISkillRepository skillRepository) : IRequestHandler<GetSkillsQuery, IReadOnlyCollection<SkillViewModel>>
{
    private readonly ISkillRepository _skillRepository = skillRepository;

    public async Task<IReadOnlyCollection<SkillViewModel>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        return await _skillRepository.Get()
                                      .AsNoTracking()
                                      .Select(s => new SkillViewModel(s.Id, s.Description))
                                      .ToListAsync(cancellationToken);
    }
}
