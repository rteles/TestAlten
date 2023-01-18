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