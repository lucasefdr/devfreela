namespace DevFreela.Application.DTOs.ViewModels.Login;

public record TokenViewModel(
    string Token,
    string RefreshToken);