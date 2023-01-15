using Alten.Booking.Application.Base.Models;
using Alten.Booking.Domain.Hotels.Entities;
using FluentValidation;

namespace Alten.Booking.Application.Hotels.ViewModels.Validators;

public class HotelValidator : ModelValidator<Hotel>
{
    public HotelValidator(IValidator<Room> roomValidator)
    {
        ClassLevelCascadeMode = CascadeMode.Continue;

        RuleFor(_ => _.Name)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.Name)} can't be null");

        RuleFor(_ => _.CreateDate)
            .NotEqual(DateTime.MinValue)
            .NotNull()
            .WithMessage(_ => $"{nameof(_.CreateDate)} can't be null");

        RuleFor(_ => _.Country)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.Country)} can't be null");

        RuleFor(_ => _.State)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.State)} can't be null");

        RuleFor(_ => _.City)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.City)} can't be null");

        RuleFor(_ => _.Address)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.Address)} can't be null");
        
        RuleForEach(_ => _.Rooms).SetValidator(roomValidator);
    }
}