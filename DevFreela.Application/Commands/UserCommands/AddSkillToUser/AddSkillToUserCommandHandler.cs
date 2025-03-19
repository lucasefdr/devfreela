using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UserCommands.AddSkillToUser;

public class AddSkillToUserCommandHandler(IUserRepository userRepository, ISkillRepository skillRepository) : IRequestHandler<AddSkillToUserCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ISkillRepository _skillRepository = skillRepository;

    public async Task Handle(AddSkillToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get()
                                        .Include(u => u.UserSkills)
                                        .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken) ?? throw new KeyNotFoundException("User not found");

        var skill = await _skillRepository.FindAsync(request.SkillId) ?? throw new KeyNotFoundException("Skill not found");

        user.AddSkill(skill);
        await _userRepository.CommitAsync();
    }
}
