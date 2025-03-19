using DevFreela.Application.ViewModels.Skill;
using MediatR;

namespace DevFreela.Application.Queries.SkillQueries.GetSkill;

public class GetSkillQuery(int id) : IRequest<SkillViewModel?>
{
    public int Id { get; set; } = id;
}
