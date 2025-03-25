using DevFreela.Application.DTOs.ViewModels.Skill;
using MediatR;

namespace DevFreela.Application.Features.Queries.SkillQueries.GetSkills;

public class GetSkillsQuery : IRequest<IReadOnlyCollection<SkillViewModel>>
{
}
