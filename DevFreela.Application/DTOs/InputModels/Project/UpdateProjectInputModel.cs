namespace DevFreela.Application.DTOs.InputModels.Project;

public record UpdateProjectInputModel(
    string Title, 
    string Description, 
    decimal TotalCost);
