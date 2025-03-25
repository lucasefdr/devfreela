using DevFreela.Core.Enums;

namespace DevFreela.Application.DTOs.ViewModels.Project;

public class ProjectDetailsViewModel
{
    public ProjectDetailsViewModel(int id, string title, decimal totalCost, string description,
                                   DateTime? startedAt, DateTime? finishedAt, DateTime? cancelledAt,
                                   ProjectStatusEnum status, string clientFullName, string freelancerFullName)
    {
        Id = id;
        Title = title;
        TotalCost = decimal.Parse(totalCost.ToString("F2")); ;
        Description = description;
        StartedAt = startedAt?.ToString("dd/MM/yyyy hh:mm:ss") ?? "Project is not started";
        FinishedAt = finishedAt?.ToString("dd/MM/yyyy hh:mm:ss") ?? "Project is not finished";
        CancelledAt = cancelledAt?.ToString("dd/MM/yyyy hh:mm:ss") ?? "Project is not cancelled";
        Status = status.ToString();

        ClientFullName = clientFullName;
        FreelancerFullName = freelancerFullName;
    }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public decimal TotalCost { get; private set; }
    public string Description { get; private set; }
    public string? StartedAt { get; private set; }
    public string? FinishedAt { get; private set; }
    public string? CancelledAt { get; private set; }
    public string? Status { get; private set; }

    public string ClientFullName { get; private set; }
    public string FreelancerFullName { get; private set; }
}
