using Alten.Booking.Application.Base.Models;
using FluentValidation;

namespace Alten.Booking.Application.Bookings.Commands.Validators;

public class ModifyBookingCommandValidator : ModelValidator<ModifyBookingCommand>
{
    public ModifyBookingCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;

        RuleFor(_ => _.BookingId)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.BookingId)} is invalid");

        RuleFor(_ => _.StartDate)
            .NotEqual(DateTime.MinValue)
            .NotNull()
            .WithMessage(_ => $"{nameof(_.StartDate)} can't be null");

        RuleFor(_ => _.EndDate)
            .NotEqual(DateTime.MinValue)
            .NotNull()
            .WithMessage(_ => $"{nameof(_.EndDate)} can't be null");
    }
}