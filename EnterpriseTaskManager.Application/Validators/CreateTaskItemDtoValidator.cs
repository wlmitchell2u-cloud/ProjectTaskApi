using FluentValidation;
using EnterpriseTaskManager.Application.DTOs.Tasks;

public class CreateTaskItemDtoValidator : AbstractValidator<CreateTaskItemDto>
{
    public CreateTaskItemDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Task title is required.")
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(1000);
        
        RuleFor(x => x.DueDateUtc)
            .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.");

        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("ProjectId must be a valid project ID.");

        RuleFor(x => x.AssignedToUserId)
            .GreaterThan(0).When(x => x.AssignedToUserId.HasValue)
            .WithMessage("AssignedToUserId must be a valid user ID.");
    }
}
