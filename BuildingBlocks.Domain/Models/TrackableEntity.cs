namespace BuildingBlocks.Domain.Models;

public abstract class TrackableEntity : Entity
{
    public DateTime CreatedAt { get; private set; }
    public string CreatedBy { get; private set; } = string.Empty;
    public DateTime? UpdatedAt { get; private set; }
    public string UpdatedBy { get; private set; } = string.Empty;
    public bool IsDeleted { get; private set; } = false;
    public DateTime? DeletedAt { get; private set; }
    public string DeletedBy { get; private set; } = string.Empty;

    public void Created(string createdBy)
    {
        CreatedAt = DateTime.Now;
        CreatedBy = createdBy;
    }

    public void Updated(string updatedBy)
    {
        UpdatedAt = DateTime.Now;
        UpdatedBy = updatedBy;
    }

    public void Deleted(string deletedBy)
    {
        IsDeleted = true;
        DeletedAt = DateTime.Now;
        DeletedBy = deletedBy;
    }
}
