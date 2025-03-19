using DevFreela.Application.Commands.ProjectCommands.CreateComment;
using FluentValidation;

namespace DevFreela.Application.Validators.Project;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .Length(10, 250).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters"); ;

        RuleFor(c => c.IdProject)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {PropertyValue}");

        RuleFor(c => c.IdUser)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {PropertyValue}");
    }
}
