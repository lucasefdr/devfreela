using DevFreela.Application.Features.Commands.UserCommands.AddSkillToUser;
using FluentValidation;

namespace DevFreela.Application.Validators.User;

public class AddSkillToUserCommandValidator : AbstractValidator<AddSkillToUserCommand>
{
    public AddSkillToUserCommandValidator()
    {
        RuleFor(s => s.UserId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {PropertyValue}");

        RuleFor(s => s.SkillId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {PropertyValue}");
    }
}
