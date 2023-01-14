namespace Alten.Booking.Domain.Base;

public interface IModelValidator<in T> where T : class
{
    bool IsValid(T model, out string[] errors);
}