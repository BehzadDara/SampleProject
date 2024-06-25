namespace BuildingBlocks.Application.Exceptions;

public class BadRequestException(Dictionary<string, string[]> errors) : Exception
{
    public Dictionary<string, string[]> Errors { get; } = errors;
}