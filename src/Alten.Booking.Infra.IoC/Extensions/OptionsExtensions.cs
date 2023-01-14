using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alten.Booking.Infra.IoC.Extensions;

[ExcludeFromCodeCoverage]
public static class OptionsExtensions
{
    public static void RegisterOptions(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // serviceCollection.Configure<SerilogSettings>(configuration.GetSection(nameof(SerilogSettings)));
        // serviceCollection.Configure<GeneralSettings>(configuration.GetSection(nameof(GeneralSettings)));
        // serviceCollection.AddSingleton<ElasticSearchSettings>();
    }
}