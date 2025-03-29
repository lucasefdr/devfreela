using DevFreela.Application.DTOs.InputModels.User;
using FluentValidation;

namespace DevFreela.Application.Validators.User;

public class CreateUserInputModelValidator : AbstractValidator<CreateUserInputModel>
{
    public CreateUserInputModelValidator()
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
            .Must(_isUnderAge).WithMessage("The user must be at least 18 years old");
    }

    // Função para validar se o usuário é maior de idade
    private readonly Func<DateTime, bool> _isUnderAge = birthDate => birthDate <= DateTime.Now.AddYears(-18);
}
