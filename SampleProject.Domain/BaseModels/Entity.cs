using System.ComponentModel.DataAnnotations;

namespace SampleProject.Domain.BaseModels;

public abstract class Entity
{
    [Key]
    public Guid Id { get; set; }
}
