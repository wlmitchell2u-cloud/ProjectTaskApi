 

namespace EnterpriseTaskManager.Application.DTOs.Comments;

public class CreateTaskCommentDto
{
    public string Message { get; set; } = string.Empty;
    public int TaskItemId { get; set; }
    public int AuthorId { get; set; }
}
