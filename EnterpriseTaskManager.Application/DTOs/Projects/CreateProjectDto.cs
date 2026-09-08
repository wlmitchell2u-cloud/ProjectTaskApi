 
namespace EnterpriseTaskManager.Application.DTOs.Projects
{
    public class CreateProjectDto
    {
        public string Name { get; set; } = string.Empty;

        public string ProjectNumber { get; set; } = string.Empty;
        public string? Description {  get; set; }

        public int OwnerId { get; set; }
    }
}
