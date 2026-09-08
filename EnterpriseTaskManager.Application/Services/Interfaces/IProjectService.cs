using EnterpriseTaskManager.Application.DTOs.Projects;

namespace EnterpriseTaskManager.Application.Services.Interfaces;

public interface IProjectService
{
    Task<List<ProjectDto>> GetAllAsync();
    Task<ProjectDto?> GetByIdAsync(int id);

    Task<ProjectDto> CreateAsync(CreateProjectDto dto);

    Task<ProjectDto?> UpdateAsync(int id, CreateProjectDto dto);

    Task<bool> DeleteAsync(int id);
}
