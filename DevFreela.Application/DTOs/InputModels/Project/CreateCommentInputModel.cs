namespace DevFreela.Application.DTOs.InputModels.Project;

public class CreateCommentInputModel
{
    public string Content { get; set; }
    public int ProjectID { get; set; }
    public int UserID { get; set; }
}
