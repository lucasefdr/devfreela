using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Application.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Queries.UserQueries.GetUser;

public class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, UserViewModel?>
{

    public async Task<UserViewModel?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return null;
    }
}
