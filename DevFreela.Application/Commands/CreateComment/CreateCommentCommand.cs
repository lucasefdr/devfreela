using MediatR;

namespace DevFreela.Application.Commands.CreateComment;

public class CreateCommentCommand : IRequest
{
    public required string Content { get; set; }
    public required int IdProject { get; set; }
    public required int IdUser { get; set; }
}
