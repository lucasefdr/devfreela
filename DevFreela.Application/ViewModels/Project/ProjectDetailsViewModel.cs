namespace DevFreela.Application.ViewModels.Project;

public class ProjectDetailsViewModel
{
    public ProjectDetailsViewModel(int id, string title, decimal totalCost, string description, DateTime? startedAt, DateTime? finishedAt)
    {
        Id = id;
        Title = title;
        TotalCost = totalCost;
        Description = description;
        StartedAt = startedAt;
        FinishedAt = finishedAt;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public decimal TotalCost { get; set; }
    public string Description { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
}
