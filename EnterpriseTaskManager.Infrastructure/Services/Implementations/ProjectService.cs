using AutoMapper;
using EnterpriseTaskManager.Application.DTOs.Projects;
using EnterpriseTaskManager.Application.Services.Interfaces;
using EnterpriseTaskManager.Domain.Entities;
using EnterpriseTaskManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseTaskManager.Infrastructure.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public ProjectService( AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<List<ProjectDto>> GetAllAsync()
    {
        var projects = await _db.Projects
            .AsNoTracking()
            .Include(p => p.Owner)
            .Include(p => p.Tasks)
            .ToListAsync();
        return _mapper.Map<List<ProjectDto>>(projects);
    }

    public async Task<ProjectDto?> GetByIdAsync(int id)
    {
        var project = await _db.Projects
            .AsNoTracking()
            .Include(p => p.Owner)
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);
        return project == null ? null : _mapper.Map<ProjectDto>(project);
    }

    public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
    {
        var project = _mapper.Map<Project>(dto);
        _db.Projects.Add(project);
        await _db.SaveChangesAsync();
        return _mapper.Map<ProjectDto>(project);

    }

    public async Task<ProjectDto?> UpdateAsync(int id, CreateProjectDto dto)
    {
        var project = await _db.Projects.FindAsync(id);
        if (project == null) return null;

        _mapper.Map(dto, project);
        await _db.SaveChangesAsync();

        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _db.Projects.FindAsync(id);
        if (project == null) return false;

        _db.Projects.Remove(project);
        await _db.SaveChangesAsync();
        return true;
    }
}
