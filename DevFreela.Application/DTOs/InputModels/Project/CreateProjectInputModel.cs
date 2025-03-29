namespace DevFreela.Application.DTOs.InputModels.Project;

public record CreateProjectInputModel(
    string Title,
    string Description,
    int ClientId,
    int FreelancerId,
    decimal TotalCost);
