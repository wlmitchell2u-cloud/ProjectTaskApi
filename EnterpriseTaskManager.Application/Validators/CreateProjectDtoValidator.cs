using FluentValidation;
using EnterpriseTaskManager.Application.DTOs.Projects;

namespace EnterpriseTaskManager.Application.Validators;

public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(100);

        RuleFor(x =>x.Description)
            .MaximumLength(500);

        RuleFor(x => x.OwnerId)
            .GreaterThan(0)
            .WithMessage("OwnerId must be a valid user ID.");
    }
}
