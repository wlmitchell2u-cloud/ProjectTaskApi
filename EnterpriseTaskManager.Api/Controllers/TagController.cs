
using EnterpriseTaskManager.Application.Services.Interfaces;
using EnterpriseTaskManager.Application.DTOs.Tags;
using Microsoft.AspNetCore.Mvc;


namespace EnterpriseTaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags(CancellationToken cancellationToken)
        {
            var tags = await _tagService.GetAllAsync(cancellationToken);
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById(int id, CancellationToken cancellationToken)
        {
            var tag = await _tagService.GetByIdAsync(id, cancellationToken);
            if (tag == null) return NotFound();

            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTagDto dto, CancellationToken cancellationToken)
        {
            var created = await _tagService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetTagById), new { id = created.Id }, created);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateTagDto dto, CancellationToken cancellationToken)
        {
            var updated = await _tagService.UpdateAsync(id, dto, cancellationToken);
            if (updated == null) return NotFound();

            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var deleted = await _tagService.DeleteAsync(id, cancellationToken);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
