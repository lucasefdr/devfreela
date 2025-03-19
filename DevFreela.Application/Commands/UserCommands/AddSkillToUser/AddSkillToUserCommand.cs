using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.AddSkillToUser;

public record AddSkillToUserCommand(int UserId, int SkillId) : IRequest
{

}
