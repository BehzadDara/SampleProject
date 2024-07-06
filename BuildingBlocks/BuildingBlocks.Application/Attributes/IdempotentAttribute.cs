namespace BuildingBlocks.Application.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class IdempotentAttribute : Attribute
{
}
