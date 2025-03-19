using DevFreela.Application.Commands.ProjectCommands.CreateProject;
using DevFreela.Application.InputModels.Project;
using FluentValidation;

namespace DevFreela.Application.Validators.Project;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(m => m.Title)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .Length(3, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(m => m.Description)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .Length(10, 250).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");

        RuleFor(m => m.IdClient)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {PropertyValue}");

        RuleFor(m => m.IdFreelancer)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {PropertyValue}");

        RuleFor(m => m.TotalCost)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {PropertyValue}");
    }
}
