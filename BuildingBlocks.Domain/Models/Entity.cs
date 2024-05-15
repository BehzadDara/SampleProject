using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Domain.Models;

public abstract class Entity
{
    [Key]
    public Guid Id { get; set; }
}
