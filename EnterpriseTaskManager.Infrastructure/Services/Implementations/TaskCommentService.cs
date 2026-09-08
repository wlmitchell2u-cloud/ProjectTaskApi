using AutoMapper;
using EnterpriseTaskManager.Infrastructure.Persistence;
using EnterpriseTaskManager.Application.DTOs.Comments;
using EnterpriseTaskManager.Domain.Entities;
using EnterpriseTaskManager.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseTaskManager.Infrastructure.Services.Implementations
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;

        public TaskCommentService(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _db = appDbContext;
        }

        public async Task<List<TaskCommentDto>> GetAllByTaskAsync(int taskId, CancellationToken cancellationToken)
        {
            var comments = await _db.TaskComments
                .AsNoTracking()
                .Where(c => c.TaskItemId == taskId)
                .Include(c => c.Author)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<TaskCommentDto>>(comments);
        }

        public async Task<TaskCommentDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var comment = await _db.TaskComments
                .AsNoTracking()
                .Include(c => c.Author)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            return comment == null ? null : _mapper.Map<TaskCommentDto>(comment);
        }

        public async Task<TaskCommentDto> CreateAsync(CreateTaskCommentDto dto, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<TaskComment>(dto);

            _db.TaskComments.Add(comment);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TaskCommentDto>(comment);
        }

        public async Task<TaskCommentDto?> UpdateAsync(int id, CreateTaskCommentDto dto, CancellationToken cancellationToken)
        {
            var comment = await _db.TaskComments.FindAsync(new object[] { id }, cancellationToken);
            if (comment == null) return null;

            _mapper.Map(dto, comment);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TaskCommentDto>(comment);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var comment = await _db.TaskComments.FindAsync(new object[] { id }, cancellationToken);
            if (comment == null) return false;
            _db.TaskComments.Remove(comment);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
