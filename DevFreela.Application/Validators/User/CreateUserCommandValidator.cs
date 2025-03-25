using DevFreela.Application.Features.Commands.UserCommands.CreateUser;
using FluentValidation;

namespace DevFreela.Application.Validators.User;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        // string FullName, string Password, string Email, DateOnly BirthDate
        RuleFor(u => u.FullName)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .Length(3, 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MinimumLength(8).WithMessage("{PropertyName} must be at least 8 characters long")
            .Matches("[A-Z]").WithMessage("{PropertyName} must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("{PropertyName} must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("{PropertyName} must contain at least one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("{PropertyName} must contain at least one special character");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .EmailAddress().WithMessage("Invalid email");

        RuleFor(u => u.BirthDate)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .LessThan(DateTime.Now).WithMessage("{PropertyName} must be in the past")
            .Must(isUnderAge).WithMessage("The user must be at least 18 years old");
    }

    private readonly Func<DateTime, bool> isUnderAge = (birthDate) => birthDate <= DateTime.Now.AddYears(-18);
}
