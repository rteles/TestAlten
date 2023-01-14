using System.Net;
using System.Text.Json.Serialization;

namespace Alten.Booking.Application.Base.Models;

public record ErrorViewModel
{
    public ErrorViewModel(HttpStatusCode statusCode, Exception exception)
    {
        Code = ((int)statusCode).ToString();
        Message = $"{statusCode} - {exception.Message}";

#if DEBUG
        StackTrace = exception.StackTrace;
#endif
    }

    public ErrorViewModel(string? code, string message)
    {
        Code = code;
        Message = message;
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Code { get; }
    public string Message { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? StackTrace { get; }
}