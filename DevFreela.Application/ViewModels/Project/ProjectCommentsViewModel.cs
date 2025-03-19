namespace DevFreela.Application.ViewModels.Project;

public record ProjectCommentsViewModel(int IdProject, string Title, IReadOnlyList<ProjectCommentViewModel> Comments);