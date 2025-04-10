using DevFreela.Core.Common;
using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities;

public class Project : BaseEntity
{
    #region Construtores
    public Project(string title, string description, int clientID, int freelancerID, decimal totalCost)
    {
        Title = title;
        Description = description;
        ClientID = clientID;
        FreelancerID = freelancerID;
        TotalCost = totalCost;

        CreatedAt = DateTime.Now;
        Status = ProjectStatusEnum.Created;
        Comments = [];
    }
    #endregion

    #region Propriedades
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int ClientID { get; private set; }
    public int FreelancerID { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }
    public DateTime? CancelledAt { get; private set; }
    public ProjectStatusEnum Status { get; private set; }
    #endregion

    #region Propriedades de navegação
    public User? Freelancer { get; }
    public User? Client { get; }
    public List<ProjectComment> Comments { get; private set; }
    #endregion

    #region Métodos
    public Result Cancel()
    {
        if (Status != ProjectStatusEnum.InProgress && Status != ProjectStatusEnum.Created)
            return Result.Failure(Error.Validation("Project.Cancel", "Project cannot be cancelled."));
        
        Status = ProjectStatusEnum.Cancelled;
        CancelledAt = DateTime.Now;
        return Result.Success();
    }

    public Result Start()
    {
        if (Status != ProjectStatusEnum.Created)
        {
            return Result.Failure(Error.Validation("Project.Start", "Project cannot be started."));
        }
        
        Status = ProjectStatusEnum.InProgress;
        StartedAt = DateTime.Now;
        return Result.Success();    
    }

    public Result Finish()
    {
        if (Status != ProjectStatusEnum.InProgress)
        {
            return Result.Failure(Error.Validation("Project.Finish", "Project cannot be finished."));
        }
        
        Status = ProjectStatusEnum.Finished;
        FinishedAt = DateTime.Now;
        return Result.Success();    
    }

    public void Update(string title, string description, decimal totalCost)
    {
        Title = title;
        Description = description;
        TotalCost = totalCost;
    }
    #endregion
}