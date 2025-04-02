using DevFreela.Application.Repositories;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Features.Commands.UserCommands.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.FullName, request.Email, request.BirthDate, request.UserType, request.Password, request.Role);

        await _userRepository.CreateAsync(user);

        return user.Id;
    }
}
