namespace SampleProject.Domain.BaseInterfaces;

public interface ICurrentUser
{
    public string IPAddress { get; }
    public string UserName { get; }
}
