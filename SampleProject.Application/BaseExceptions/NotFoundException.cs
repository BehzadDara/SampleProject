namespace SampleProject.Application.BaseExceptions;

public class NotFoundException : Exception
{
    protected NotFoundException() : base(Resources.Messages.NotFound)
    {

    }
}
