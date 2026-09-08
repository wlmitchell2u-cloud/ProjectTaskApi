using FluentValidation;
using EnterpriseTaskManager.Application.DTOs.Comments;

public class CreateTaskCommentDtoValidator : AbstractValidator<CreateTaskCommentDto>
{
    public CreateTaskCommentDtoValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Comment message is required.")
            .MaximumLength(1000).WithMessage("Comment cannot exceed 1000 characters.");

        RuleFor(x => x.TaskItemId)
            .GreaterThan(0).WithMessage("TaskItemId must be a valid task ID.");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0).WithMessage("AuthorId must be a valid user ID.");
    }
}

