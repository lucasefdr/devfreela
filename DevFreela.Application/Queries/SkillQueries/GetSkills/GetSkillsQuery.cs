using DevFreela.Application.ViewModels.Skill;
using MediatR;

namespace DevFreela.Application.Queries.SkillQueries.GetSkills;

public class GetSkillsQuery : IRequest<IReadOnlyCollection<SkillViewModel>>
{
}
