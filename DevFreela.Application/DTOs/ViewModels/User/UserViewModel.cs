namespace DevFreela.Application.DTOs.ViewModels.User;

public record UserViewModel(int Id, string FullName, string IsActive, List<string> Skills);

public record FreelancerViewModel(int Id, string FullName, string IsActive, List<string> Skills);

public record ClientViewModel(int Id, string FullName, string IsActive, List<string> OwnedProjects);