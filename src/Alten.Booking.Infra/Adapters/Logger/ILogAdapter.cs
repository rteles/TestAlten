namespace Alten.Booking.Infra.Adapters.Logger;

public interface ILogAdapter
{
    void Debug(string message, object? structuredData = default);
    void Info(string message, object? structuredData = default);
    void Warning(string message, object? structuredData = default);
    void Error(string message, object? structuredData = default);
    void Fatal(string message, object? structuredData = default);
}