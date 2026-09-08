using EnterpriseTaskManager.Api.Hubs;
using EnterpriseTaskManager.Application.DTOs.Projects;
using EnterpriseTaskManager.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.InteropServices;


namespace EnterpriseTaskManager.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IHubContext<ProjectHub> _hubContext;
        public ProjectsController(IProjectService projectService, IHubContext<ProjectHub> hubContext)
        {
            _projectService = projectService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

           var projects = await _projectService.GetAllAsync();

           return Ok(projects);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto dto)
        {
            var created = await _projectService.CreateAsync(dto);

            //Broadcast to all connected clients that a project was created
            await _hubContext.Clients.All.SendAsync("ProjectCreated", created);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateProjectDto dto)
        {
            var updated = await _projectService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();

            await _hubContext.Clients.All.SendAsync("ProjectUpdated", updated);

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _projectService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            await _hubContext.Clients.All.SendAsync("ProjectDeleted", id);

            return NoContent();
        }
    }
}
