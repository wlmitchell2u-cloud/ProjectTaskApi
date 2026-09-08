namespace EnterpriseTaskManager.Application.DTOs.Comments;

public class TaskCommentDto
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;

    public int TaskItemId { get; set; }

    public int AuthorId { get; set; }
}
