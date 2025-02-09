namespace DevFreela.Application.ViewModels.Project;

public class ProjectViewModel
{
    public int Id { get; set; }
    public string Title { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ProjectViewModel(int id, string title, DateTime createdAt)
    {
        Id = id;
        Title = title;
        CreatedAt = createdAt;
    }
}
