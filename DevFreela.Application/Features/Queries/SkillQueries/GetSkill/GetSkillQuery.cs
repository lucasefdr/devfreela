using DevFreela.Application.DTOs.ViewModels.Skill;
using MediatR;

namespace DevFreela.Application.Features.Queries.SkillQueries.GetSkill;

public class GetSkillQuery(int id) : IRequest<SkillViewModel?>
{
    public int Id { get; set; } = id;
}
