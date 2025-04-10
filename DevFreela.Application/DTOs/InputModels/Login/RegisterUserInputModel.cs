using DevFreela.Core.Enums;

namespace DevFreela.Application.DTOs.InputModels.Login;

public record RegisterUserInputModel(
    string FullName,
    string UserName,
    string Email,
    DateTime BirthDate,
    UserTypeEnum UserType,
    string Password);