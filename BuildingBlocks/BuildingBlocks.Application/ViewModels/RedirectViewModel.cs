namespace BuildingBlocks.Application.ViewModels;

public class RedirectViewModel
{
    public required string URL { get; set; }
    public string Token { get; set; } = string.Empty;
}