namespace DevFreela.Application.ViewModels.User;

public record UserSkillsViewModel(string FullName, string email, IReadOnlyCollection<string> Skills);