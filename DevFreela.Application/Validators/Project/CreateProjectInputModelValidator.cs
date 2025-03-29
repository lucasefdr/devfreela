using DevFreela.Application.DTOs.InputModels.Project;
using FluentValidation;

namespace DevFreela.Application.Validators.Project;

public class CreateProjectInputModelValidator : AbstractValidator<CreateProjectInputModel>
{
    public CreateProjectInputModelValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(5, 50).WithMessage("{PropertyName} must be between 5 and 50 characters.");

        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(5, 100).WithMessage("{PropertyName} must be between 5 and 100 characters.");

        RuleFor(c => c.ClientId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

        RuleFor(c => c.ClientId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

        RuleFor(c => c.TotalCost)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
    }
}