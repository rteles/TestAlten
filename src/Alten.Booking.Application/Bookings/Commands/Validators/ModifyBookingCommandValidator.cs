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

        RuleFor(_ => _.CheckinDate)
            .NotEqual(DateTime.MinValue)
            .NotNull()
            .WithMessage(_ => $"{nameof(_.CheckinDate)} can't be null");

        RuleFor(_ => _.CheckoutDate)
            .NotEqual(DateTime.MinValue)
            .NotNull()
            .WithMessage(_ => $"{nameof(_.CheckoutDate)} can't be null");
    }
}