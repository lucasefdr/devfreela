namespace DevFreela.Application.DTOs.ViewModels.Project;

public record ProjectCommentsViewModel(int IdProject, string Title, IReadOnlyList<ProjectCommentViewModel> Comments);