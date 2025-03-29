using DevFreela.Application.Repositories;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Features.Commands.ProjectCommands.CreateComment;

public class CreateCommentCommandHandler(IProjectRepository projectRepository) : IRequestHandler<CreateCommentCommand>
{


    public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        //var comment = new ProjectComment(request.Content, request.IdProject, request.UserId);
        //await projectRepository..AddAsync(comment, cancellationToken);
        //await projectRepository.CommitAsync(cancellationToken);
    }
}
