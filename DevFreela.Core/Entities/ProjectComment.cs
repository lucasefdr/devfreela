using DevFreela.Core.Common;
using System.Text.Json.Serialization;

namespace DevFreela.Core.Entities;

public class ProjectComment : BaseEntity
{
    #region Construtores
    public ProjectComment(string content, int projectId, int userId)
    {
        Content = content;
        ProjectId = projectId;
        UserId = userId;

        CreatedAt = DateTime.Now;
    }
    #endregion

    #region Propriedades
    public string Content { get; private set; }
    public int ProjectId { get; private set; }
    public int UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    #endregion

    #region Propriedades de navegação
    public Project Project { get; } = null!;
    public User User { get; } = null!;
    #endregion


}