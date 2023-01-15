using Alten.Booking.Application.Base.Models;
using Alten.Booking.Domain.Users.Entities;
using FluentValidation;

namespace Alten.Booking.Application.Users.ViewModels.Validators;

public class UserValidator : ModelValidator<User>
{
    public UserValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;

        RuleFor(_ => _.Name)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.Name)} can't be null");

        RuleFor(_ => _.LastName)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.LastName)} can't be null");

        RuleFor(_ => _.BirthDate)
            .NotEqual(DateTime.MinValue)
            .NotNull()
            .WithMessage(_ => $"{nameof(_.BirthDate)} can't be null");

        RuleFor(_ => _.Email)
            .NotEmpty()
            .WithMessage(_ => $"{nameof(_.Email)} can't be null")
            .EmailAddress()
            .WithMessage(_ => $"{nameof(_.Email)} is invalid");
    }
}