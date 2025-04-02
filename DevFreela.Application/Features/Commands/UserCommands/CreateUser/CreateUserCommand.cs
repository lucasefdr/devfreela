using DevFreela.Core.Enums;
using MediatR;

namespace DevFreela.Application.Features.Commands.UserCommands.CreateUser;

public record CreateUserCommand(
    string FullName,
    string Email,
    DateTime BirthDate,
    UserTypeEnum UserType,
    string Password,
    string Role) : IRequest<int>;