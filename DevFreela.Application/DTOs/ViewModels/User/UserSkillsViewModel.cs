namespace DevFreela.Application.DTOs.ViewModels.User;

public record UserSkillsViewModel(string FullName, string email, IReadOnlyCollection<string> Skills);