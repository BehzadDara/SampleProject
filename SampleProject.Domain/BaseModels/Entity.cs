using System.ComponentModel.DataAnnotations;

namespace SampleProject.Domain.BaseModels;

public class Entity
{
    [Key]
    public Guid Id { get; set; }
}
