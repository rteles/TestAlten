using Alten.Booking.Application.Base.Models;
using FluentValidation;

namespace Alten.Booking.Application.Bookings.Commands.Validators;

public class CancelBookingCommandValidator : ModelValidator<CancelBookingCommand>
{
    public CancelBookingCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;

        RuleFor(_ => _.BookingId)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.BookingId)} is invalid");
    }
}