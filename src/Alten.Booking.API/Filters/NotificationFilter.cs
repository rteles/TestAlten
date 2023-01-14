using System.Diagnostics.CodeAnalysis;
using Alten.Booking.Application.Extensions;
using Alten.Booking.Core.Notifications.Interfaces;
using Alten.Booking.Infra.Adapters.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Alten.Booking.API.Filters;

[ExcludeFromCodeCoverage]
public class NotificationFilter : IActionFilter
{
    private const string LogPrefix = $"[{nameof(NotificationFilter)}]";
    private readonly ILogAdapter _logger;
    // private readonly IMonitoringAdapter _monitoring;
    private readonly INotificationContext _notificationContext;

    public NotificationFilter(ILogAdapter logger, 
        // IMonitoringAdapter monitoring, 
        INotificationContext notificationContext)
    {
        _logger = logger;
        // _monitoring = monitoring;
        _notificationContext = notificationContext;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //No action required here
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (!_notificationContext.HasNotifications) return;
        var notifications = _notificationContext.Notifications.ToErrorViewModel();
        _logger.Warning(LogPrefix, notifications);
        // _monitoring.IncrementCounter(MonitoringIndicators.NotificationsCount);
        context.Result = new BadRequestObjectResult(notifications);
    }
}