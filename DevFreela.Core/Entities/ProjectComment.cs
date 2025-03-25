using DevFreela.Core.Common;
using System.Text.Json.Serialization;

namespace DevFreela.Core.Entities;

public class ProjectComment : BaseEntity
{
    #region Construtores
    public ProjectComment(string content, int projectID, int userID)
    {
        Content = content;
        ProjectID = projectID;
        UserID = userID;

        CreatedAt = DateTime.Now;
    }
    #endregion

    #region Propriedades
    public string Content { get; private set; }
    public int ProjectID { get; private set; }
    public int UserID { get; private set; }
    public DateTime CreatedAt { get; private set; }
    #endregion

    #region Propriedades de navegação
    public Project Project { get; } = null!;
    public User User { get; } = null!;
    #endregion


}