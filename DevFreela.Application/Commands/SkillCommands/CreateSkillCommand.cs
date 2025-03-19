using MediatR;

namespace DevFreela.Application.Commands.SkillCommands;

public record CreateSkillCommand(string Description) : IRequest<int>
{
}
