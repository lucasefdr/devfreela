namespace DevFreela.Application.ViewModels.User;

public record UserViewModel(string FullName, string Email, IReadOnlyCollection<string> Skills);