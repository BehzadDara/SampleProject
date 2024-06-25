namespace BuildingBlocks.Domain.Interfaces;

public interface ICurrentUser
{
    public string IPAddress { get; }
    public string UserName { get; }
}
