using DevFreela.Application.DTOs.InputModels.Skill;
using FluentValidation;

namespace DevFreela.Application.Validators.Skill;

public class SkillInputModelValidator : AbstractValidator<SkillInputModel>
{
    public SkillInputModelValidator()
    {
        RuleFor(s => s.Description)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .Length(3, 250).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters");
    }
}