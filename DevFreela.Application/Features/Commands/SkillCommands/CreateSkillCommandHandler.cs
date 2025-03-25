using DevFreela.Application.Repositories;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Features.Commands.SkillCommands;

public class CreateSkillCommandHandler(ISkillRepository skillRepository) : IRequestHandler<CreateSkillCommand, int>
{
    private readonly ISkillRepository _skillRepository = skillRepository;

    public async Task<int> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = new Skill(request.Description);

        await _skillRepository.CreateAsync(skill);

        return skill.ID;
    }
}
