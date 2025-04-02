using DevFreela.Core.Enums;

namespace DevFreela.Application.DTOs.InputModels.User;

public record CreateUserInputModel(
    string FullName,
    string Email,
    DateTime BirthDate,
    UserTypeEnum UserType,
    string Password);