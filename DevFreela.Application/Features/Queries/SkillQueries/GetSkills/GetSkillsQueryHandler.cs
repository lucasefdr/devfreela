using DevFreela.Application.DTOs.ViewModels.Skill;
using DevFreela.Application.Repositories;
using MediatR;
using System.Collections.ObjectModel;

namespace DevFreela.Application.Features.Queries.SkillQueries.GetSkills;

public class GetSkillsQueryHandler(ISkillRepository skillRepository)
    : IRequestHandler<GetSkillsQuery, IReadOnlyCollection<SkillViewModel>>
{

    public async Task<IReadOnlyCollection<SkillViewModel>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        //var skills = await skillRepository.GetAllAsync();
        var mock = new List<SkillViewModel>();

        return mock;
            //[.. skills.Select(s => new SkillViewModel(s.ID, s.Description))];

    }
}
