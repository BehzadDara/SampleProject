using FluentValidation.Results;

namespace SampleProject.Application.BaseFeatures;

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
    #region Properties
    public bool IsSuccess { get; set; }

    public List<string> Errors { get; set; } = [];

    public Dictionary<string, List<string>> ValidationErrors { get; set; } = [];

    public List<string> Successes { get; set; } = [];
    #endregion


    #region SuccessMessage
    public void AddSuccessMessage(string message)
    {
        if (!string.IsNullOrEmpty(message) && !Successes.Contains(message))
        {
            Successes.Add(message);
        }
    }
    #endregion

    #region ErrorMessage
    public void AddErrorMessage(string message)
    {
        if (!string.IsNullOrEmpty(message) && !Errors.Contains(message))
        {
            Errors.Add(message);
        }
    }
    #endregion

    #region ValidationErrorMessage
    public void AddValidationErrorMessage(string key, string message)
    {
        if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(message))
        {
            if (!ValidationErrors.TryGetValue(key, out List<string>? value))
            {
                value = ([]);
                ValidationErrors[key] = value;
            }

            value.Add(message);
        }
    }

    public void AddValidationErrorMessages(List<ValidationFailure> messages)
    {
        messages.ForEach(message => AddValidationErrorMessage(message.PropertyName, message.ErrorMessage));
    }
    #endregion


    #region OK
    public void OK()
    {
        Succeed(Resources.Messages.SuccessAction);
    }
    #endregion

    #region BadRequest
    public void BadRequest(List<ValidationFailure> errors)
    {
        Failed(Resources.Messages.BadRequest, errors);
    }
    #endregion

    #region Unauthorized
    public void Unauthorized()
    {
        Failed(Resources.Messages.Unauthorized);
    }
    #endregion

    #region Forbidden
    public void Forbidden()
    {
        Failed(Resources.Messages.Forbidden);
    }
    #endregion

    #region NotFound
    public void NotFound()
    {
        Failed(Resources.Messages.NotFound);
    }
    #endregion

    #region MethodNotAllowed
    public void MethodNotAllowed()
    {
        Failed(Resources.Messages.MethodNotAllowed);
    }
    #endregion

    #region TooManyRequest
    public void TooManyRequest()
    {
        Failed(Resources.Messages.TooManyRequest);
    }
    #endregion

    #region InternalServerError
    public void InternalServerError()
    {
        Failed(Resources.Messages.InternalServerError);
    }
    #endregion


    #region IsSuccess
    private void Succeed(string message)
    {
        AddSuccessMessage(message);
        IsSuccess = true;
    }

    private void Failed(string message)
    {
        AddErrorMessage(message);
        IsSuccess = false;
    }

    private void Failed(string message, List<ValidationFailure> errors)
    {
        AddErrorMessage(message);
        AddValidationErrorMessages(errors);
        IsSuccess = false;
    }
    #endregion
}
