using Alten.Booking.Application.Base.Models;
using Alten.Booking.Domain.Hotels.Entities;
using FluentValidation;

namespace Alten.Booking.Application.Hotels.ViewModels.Validators;

public class RoomValidator : ModelValidator<Room>
{
    public RoomValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;

        RuleFor(_ => _.Hotel)
            .NotNull()
            .WithMessage(_ => $"{nameof(_.Hotel)} can't be null");

        RuleFor(_ => _.Number)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.Number)} can't be null");

        RuleFor(_ => _.PricePerDay)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.PricePerDay)} can't be null");

        RuleFor(_ => _.RoomType)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.RoomType)} can't be null");

        RuleFor(_ => _.RoomType)
            .IsInEnum()
            .WithMessage(_ => $"{nameof(_.RoomType)} is invalid");
    }
}