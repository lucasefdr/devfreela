using DevFreela.Application.DTOs.ViewModels.User;
using DevFreela.Application.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Queries.UserQueries.GetUser;

public class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, UserViewModel?>
{

    public async Task<UserViewModel?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithSkillsAsync(request.Id);

        if (user == null) return null;

        return new UserViewModel(user.ID, user.FullName, user.Email, user.IsActive.ToString(), [.. user.UserSkills.Select(us => us.Skill!.Description)]);
    }
}
