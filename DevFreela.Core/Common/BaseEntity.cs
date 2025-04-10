namespace DevFreela.Core.Common;

public abstract class BaseEntity
{
    public int Id { get; protected set; }

    protected BaseEntity() { }

    public void SetId(int id)
    {
        Id = id;
    }
}
