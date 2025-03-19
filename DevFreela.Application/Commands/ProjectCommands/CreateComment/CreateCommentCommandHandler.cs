using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
{
    private readonly DevFreelaDbContext _context;

    public CreateCommentCommandHandler(DevFreelaDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);
        await _context.Comments.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
