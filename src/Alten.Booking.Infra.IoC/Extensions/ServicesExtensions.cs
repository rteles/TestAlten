using System.Diagnostics.CodeAnalysis;
using Alten.Booking.Application.Hotels.Commands;
using Alten.Booking.Application.Hotels.Commands.Handlers;
using Alten.Booking.Application.Hotels.Queries;
using Alten.Booking.Application.Hotels.Queries.Interfaces;
using Alten.Booking.Application.Hotels.ViewModels.Validators;
using Alten.Booking.Application.Users.Commands;
using Alten.Booking.Application.Users.Commands.Handlers;
using Alten.Booking.Application.Users.Queries;
using Alten.Booking.Application.Users.Queries.Interfaces;
using Alten.Booking.Application.Users.ViewModels.Validators;
using Alten.Booking.Core.Mediator;
using Alten.Booking.Core.Mediator.Interfaces;
using Alten.Booking.Core.Messages;
using Alten.Booking.Core.Notifications;
using Alten.Booking.Core.Notifications.Interfaces;
using Alten.Booking.Domain.Base;
using Alten.Booking.Domain.Hotels.Entities;
using Alten.Booking.Domain.Hotels.Repositories;
using Alten.Booking.Domain.Users.Entities;
using Alten.Booking.Domain.Users.Repositories;
using Alten.Booking.Infra.Adapters.Logger;
using Alten.Booking.Infra.Data.Sql.Context;
using Alten.Booking.Infra.Data.Sql.Context.Hotels.Repositories;
using Alten.Booking.Infra.Data.Sql.Context.Users.Repositories;
using Alten.Booking.Infra.IoC.AutoMapper.Profiles;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alten.Booking.Infra.IoC.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesExtensions
{
    public static void RegisterServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDatabases(configuration);
        serviceCollection.AddMonitoring();
        serviceCollection.AddMediator();
        serviceCollection.AddAutoMapperProfiles();
        serviceCollection.AddValidators();
        serviceCollection.AddRepositories();
        serviceCollection.AddApplication();
    }

    private static void AddDatabases(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<HotelBookingContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("HotelBooking")));

        serviceCollection.AddDatabaseDeveloperPageExceptionFilter();
    }

    private static void AddAutoMapperProfiles(this IServiceCollection serviceCollection)
    {
        // Auto Mapper Configurations
        serviceCollection.AddAutoMapper(_ =>
        {
            _.AddProfile<DomainToModelProfile>();
            _.AddProfile<DomainToViewModelProfile>();
        });
    }

    private static void AddMonitoring(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ILogAdapter, SerilogAdapter>();
        // serviceCollection.AddSingleton<IMetricsRegistry, MetricsRegistry>();
        // serviceCollection.AddTransient<IMonitoringAdapter, MetricsRegistryAdapter>();
    }

    private static void AddMediator(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMediatorHandler, MediatorHandler>();
        serviceCollection.AddScoped<INotificationHandler<Notification>, NotificationHandler>();
        serviceCollection.AddScoped<INotificationContext, NotificationContext>();
    }

    private static void AddValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidationAutoValidation();
        serviceCollection.AddScoped<IModelValidator<User>, UserValidator>();
        serviceCollection.AddScoped<IModelValidator<Hotel>, HotelValidator>();
        serviceCollection.AddScoped<IModelValidator<Room>, RoomValidator>();
        serviceCollection.AddScoped<IValidator<Room>, RoomValidator>();
    }

    private static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IHotelRepository, HotelRepository>();
        serviceCollection.AddScoped<DbContext, HotelBookingContext>();
    }

    private static void AddApplication(this IServiceCollection serviceCollection)
    {
        //Queries
        serviceCollection.AddScoped<IUserQueries, UserQueries>();
        serviceCollection.AddScoped<IHotelQueries, HotelQueries>();

        //Commands
        serviceCollection.AddScoped<IRequestHandler<AddUserCommand, Unit>, AddUserCommandHandler>();
        serviceCollection.AddScoped<IRequestHandler<AddHotelCommand, Unit>, AddHotelCommandHandler>();

        //Events
    }
}