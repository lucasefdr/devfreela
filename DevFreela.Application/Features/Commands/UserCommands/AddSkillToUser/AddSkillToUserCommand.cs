using MediatR;

namespace DevFreela.Application.Features.Commands.UserCommands.AddSkillToUser;

public record AddSkillToUserCommand(int UserId, int SkillId) : IRequest
{

}
