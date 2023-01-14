using Alten.Booking.Domain.Base;
using FluentValidation;

namespace Alten.Booking.Application.Base.Models;

public abstract class ModelValidator<T> : AbstractValidator<T>, IModelValidator<T> where T : class
{
    public bool IsValid(T model, out string[] errors)
    {
        var result = Validate(model);

        errors = result.Errors.Any()
            ? result.Errors.Select(_ => _.ErrorMessage).ToArray()
            : Array.Empty<string>();

        return result.IsValid;
    }
}