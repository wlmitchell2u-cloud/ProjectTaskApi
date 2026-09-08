
namespace EnterpriseTaskManager.Application.DTOs.Projects
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string ProjectNumber { get; set; } = string.Empty;
        public string? Description {  get; set; }
        public int Status { get; set; }
        public int OwnerId { get; set; }
    }
}
