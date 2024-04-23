namespace SampleProject.API.BaseExceptions;

public class NotFoundException : Exception
{
    protected NotFoundException() : base(Resources.Messages.NotFound)
    {

    }

    protected NotFoundException(string message) : base(message)
    {

    }
}
