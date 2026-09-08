using EnterpriseTaskManager.Application.DTOs.Comments;
using EnterpriseTaskManager.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseTaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskCommentsController : ControllerBase
    {
        private readonly ITaskCommentService _taskCommentService;

        public TaskCommentsController(ITaskCommentService taskCommentService)
        {
            _taskCommentService = taskCommentService;
        }

        [HttpGet("task/{taskId:int}")]
        public async Task<IActionResult> GetAllByTaskAsync(int taskId,CancellationToken cancellationToken)
        {
            var comments = await _taskCommentService.GetAllByTaskAsync(taskId, cancellationToken);            
            return Ok(comments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var comment = await _taskCommentService.GetByIdAsync(id, cancellationToken);
            if (comment == null) return NotFound();

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskCommentDto dto, CancellationToken cancellationToken)
        {
            var created = await _taskCommentService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateTaskCommentDto dto, CancellationToken cancellationToken)
        {
            var updated = await _taskCommentService.UpdateAsync(id, dto, cancellationToken);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var deleted = await _taskCommentService.DeleteAsync(id, cancellationToken);

            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
