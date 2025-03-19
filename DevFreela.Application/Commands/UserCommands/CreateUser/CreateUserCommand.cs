using MediatR;

namespace DevFreela.Application.Commands.UserCommands.CreateUser;

public record CreateUserCommand(string FullName, string Password, string Email, DateTime BirthDate) : IRequest<int>
{ }

