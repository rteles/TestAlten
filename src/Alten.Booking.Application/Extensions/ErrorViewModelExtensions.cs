using System.Net;
using Alten.Booking.Application.Base.Models;
using Alten.Booking.Core.Messages;

namespace Alten.Booking.Application.Extensions;

public static class ErrorViewModelExtensions
{
    public static ErrorViewModel ToErrorViewModel(this Notification notification) => 
        new(notification.Code, notification.Message);

    public static ErrorViewModel ToErrorViewModel(this Exception exception, HttpStatusCode statusCode) =>
        new(statusCode, exception);

    public static IEnumerable<ErrorViewModel> ToErrorViewModel(this IEnumerable<Notification> notifications) => 
        notifications.Select(notification => notification.ToErrorViewModel());
}