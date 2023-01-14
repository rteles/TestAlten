using Serilog;

namespace Alten.Booking.Infra.Adapters.Logger;

public class SerilogAdapter : ILogAdapter
{
    public void Debug(string message, object? structuredData = default) =>
        Log.Debug(message + " {@StructuredData}", structuredData);

    public void Info(string message, object? structuredData = default) =>
        Log.Information(message + " {@StructuredData}", structuredData);

    public void Warning(string message, object? structuredData = default) =>
        Log.Warning(message + " {@StructuredData}", structuredData);

    public void Error(string message, object? structuredData = default) =>
        Log.Error(message + " {@StructuredData}", structuredData);

    public void Fatal(string message, object? structuredData = default) =>
        Log.Fatal(message + " {@StructuredData}", structuredData);
}