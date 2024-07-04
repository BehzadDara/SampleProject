namespace BuildingBlocks.Application.Exceptions;

public class BadRequestException(string error, Dictionary<string, string[]> errors) : Exception
{
    public string Error { get; } = error;
    public Dictionary<string, string[]> Errors { get; } = errors;
}