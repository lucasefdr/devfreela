using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.SkillCommands;

public class CreateSkillCommandHandler(ISkillRepository skillRepository) : IRequestHandler<CreateSkillCommand, int>
{
    private readonly ISkillRepository _skillRepository = skillRepository;

    public async Task<int> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = new Skill(request.Description);

        await _skillRepository.CreateAsync(skill);

        return skill.Id;
    }
}
