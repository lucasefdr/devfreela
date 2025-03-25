using MediatR;

namespace DevFreela.Application.Features.Commands.ProjectCommands.CreateComment;

public record CreateCommentCommand(string Content, int IdProject, int IdUser) : IRequest
{
}
