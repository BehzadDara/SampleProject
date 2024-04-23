namespace SampleProject.Domain.BaseModels;

public abstract class TrackableEntity : Entity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
}
