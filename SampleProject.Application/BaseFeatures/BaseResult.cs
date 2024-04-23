namespace SampleProject.Application.BaseFeature;

public class BaseResult<T> : BaseResult
{
    public T? Value { get; private set; }

    public void AddValue(T value)
    {
        Value = value;
    }
}

public class BaseResult
{
    public bool IsSuccess { get; set; }

    public List<string> Errors { get; set; } = [];

    public List<string> Successes { get; set; } = [];

    public void AddErrorMessage(string message)
    {
        if (message != null && !Errors.Contains(message))
        {
            Errors.Add(message);
            Failed();
        }
    }

    public void AddErrorMessages(List<string> messages)
    {
        messages.ForEach(AddErrorMessage);
    }

    public void AddSuccessMessage(string message)
    {
        if (message != null && !Successes.Contains(message))
        {
            Successes.Add(message);
            Succeed();
        }
    }

    public void AddSuccessMessages(List<string> messages)
    {
        messages.ForEach(AddSuccessMessage);
    }

    private void Failed()
    {
        IsSuccess = false;
    }

    private void Succeed()
    {
        IsSuccess = true;
    }
}
