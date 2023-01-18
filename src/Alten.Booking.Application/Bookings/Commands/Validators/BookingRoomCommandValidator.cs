using Alten.Booking.Application.Base.Models;
using FluentValidation;

namespace Alten.Booking.Application.Bookings.Commands.Validators;

public class BookingRoomCommandValidator : ModelValidator<BookingRoomCommand>
{
    public BookingRoomCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;

        RuleFor(_ => _.RoomId)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.RoomId)} is invalid");

        RuleFor(_ => _.UserId)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.UserId)} is invalid");

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