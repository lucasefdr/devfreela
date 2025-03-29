namespace DevFreela.Application.DTOs.InputModels.Project;

public record CreateCommentInputModel(
    string Content, 
    int ProjectId, 
    int UserId);
