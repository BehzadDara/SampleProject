using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

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

    [JsonIgnore] public int StatusCode { get; set; }

    public List<string> Errors { get; set; } = [];

    public Dictionary<string, List<string>> ValidationErrors { get; set; } = [];

    public List<string> Successes { get; set; } = [];
    #endregion

    #region ErrorMessage
    public void AddErrorMessage(string message)
    {
        if (!string.IsNullOrEmpty(message) && !Errors.Contains(message))
        {
            Errors.Add(message);
            Failed();
        }
    }

    public void AddErrorMessages(List<string> messages)
    {
        messages.ForEach(AddErrorMessage);
    }
    #endregion

    #region SuccessMessage
    public void AddSuccessMessage(string message)
    {
        if (!string.IsNullOrEmpty(message) && !Successes.Contains(message))
        {
            Successes.Add(message);
            Succeed();
        }
    }

    public void AddSuccessMessages(List<string> messages)
    {
        messages.ForEach(AddSuccessMessage);
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
            Failed();
        }
    }
    public void AddValidationErrorMessages(List<ValidationFailure> messages)
    {
        messages.ForEach(message => AddValidationErrorMessage(message.PropertyName, message.ErrorMessage));
    }

    #endregion

    #region Success
    public void Success()
    {
        Success(Resources.Messages.SuccessAction);
    }

    public void Success(string message)
    {
        StatusCode = StatusCodes.Status200OK;
        AddSuccessMessage(message);
    }
    #endregion

    #region NotFound
    public void NotFound()
    {
        NotFound(Resources.Messages.NotFound);
    }

    public void NotFound(string message)
    {
        StatusCode = StatusCodes.Status404NotFound;
        AddErrorMessage(message);
    }
    #endregion

    #region BadRequest
    public void BadRequest(List<ValidationFailure> errors)
    {
        BadRequest(errors, Resources.Messages.BadRequest);
    }

    public void BadRequest(List<ValidationFailure> errors, string message)
    {
        StatusCode = StatusCodes.Status400BadRequest;
        AddValidationErrorMessages(errors);
        AddErrorMessage(message);
    }
    #endregion

    #region Unauthorized
    public void Unauthorized()
    {
        Unauthorized(Resources.Messages.Unauthorized);
    }

    public void Unauthorized(string message)
    {
        StatusCode = StatusCodes.Status401Unauthorized;
        AddErrorMessage(message);
    }
    #endregion

    #region Forbidden
    public void Forbidden()
    {
        Forbidden(Resources.Messages.Forbidden);
    }

    public void Forbidden(string message)
    {
        StatusCode = StatusCodes.Status403Forbidden;
        AddErrorMessage(message);
    }
    #endregion

    #region InternalServerError
    public void InternalServerError()
    {
        InternalServerError(Resources.Messages.InternalServerError);
    }

    public void InternalServerError(string message)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
        AddErrorMessage(message);
    }
    #endregion

    #region IsSuccess
    private void Succeed()
    {
        IsSuccess = true;
    }

    private void Failed()
    {
        IsSuccess = false;
    }
    #endregion
}
