using DevFreela.Core.Enums;
using MediatR;

namespace DevFreela.Application.Features.Commands.UserCommands.CreateUser;

public record CreateUserCommand(string FullName, string Password, string Email, DateTime BirthDate, UserTypeEnum UserType) : IRequest<int>
{ }

