using DevFreela.Application.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Commands.UserCommands.AddSkillToUser;

public class AddSkillToUserCommandHandler(IUserRepository userRepository, ISkillRepository skillRepository) : IRequestHandler<AddSkillToUserCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ISkillRepository _skillRepository = skillRepository;

    public async Task Handle(AddSkillToUserCommand request, CancellationToken cancellationToken)
    {
        // var user = await _userRepository.GetUserWithSkillsAsync(request.UserId);
        //
        // var skill = await _skillRepository.FindAsync(request.SkillId);
        //
        // user.AddSkill(skill);
        // await _userRepository.CommitAsync();
    }
}
