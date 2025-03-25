using MediatR;

namespace DevFreela.Application.Features.Commands.SkillCommands;

public record CreateSkillCommand(string Description) : IRequest<int>
{
}
