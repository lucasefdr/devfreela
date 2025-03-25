namespace DevFreela.Application.DTOs.InputModels.Project;

public class CreateProjectInputModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int ClientID { get; set; }
    public int FreelancerID { get; set; }
    public decimal TotalCost { get; set; }
}
