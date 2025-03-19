using DevFreela.Application.Commands.SkillCommands;
using FluentValidation;

namespace DevFreela.Application.Validators.Skill;

public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
{
    public CreateSkillCommandValidator()
    {
        RuleFor(s => s.Description)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .Length(10, 250).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");
    }
}
