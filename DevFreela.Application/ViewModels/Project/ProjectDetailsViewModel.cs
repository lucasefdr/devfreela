namespace DevFreela.Application.ViewModels.Project;

public class ProjectDetailsViewModel
{
    public ProjectDetailsViewModel(int id, string title, decimal totalCost, string description, DateTime? startedAt, DateTime? finishedAt,
        string clientFullName, string freelancerFullName)
    {
        Id = id;
        Title = title;
        TotalCost = totalCost;
        Description = description;
        StartedAt = startedAt?.ToString("dd/MM/yyyy hh:mm:ss") ?? "Project is not started";
        FinishedAt = finishedAt?.ToString("dd/MM/yyyy hh:mm:ss") ?? "Project is not finished";

        ClientFullName = clientFullName;
        FreelancerFullName = freelancerFullName;
    }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public decimal TotalCost { get; private set; }
    public string Description { get; private set; }
    public string? StartedAt { get; private set; } 
    public string? FinishedAt { get; private set; } 

    public string ClientFullName { get; private set; }
    public string FreelancerFullName { get; private set; }
}
