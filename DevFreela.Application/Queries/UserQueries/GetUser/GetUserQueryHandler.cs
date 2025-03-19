using DevFreela.Application.ViewModels.User;
using DevFreela.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.UserQueries.GetUser;

public class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, UserViewModel?>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserViewModel?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get()
                                        .Include(u => u.UserSkills)
                                            .ThenInclude(s => s.Skill)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(u => u.Id == request.Id) ?? throw new KeyNotFoundException("User not found");

        return new UserViewModel(user.FullName, user.Email, user.UserSkills.Select(us => us.Skill.Description).ToList());
    }
}
