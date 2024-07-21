namespace AnotherSampleProject.API.Services;

public interface IRabbitMQService
{
    void SendAddTestModelMessage(string name, CancellationToken cancellationToken);
}
