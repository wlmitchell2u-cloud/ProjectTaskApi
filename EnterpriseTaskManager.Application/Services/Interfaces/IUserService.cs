using EnterpriseTaskManager.Application.DTOs.Users;

namespace EnterpriseTaskManager.Application.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<UserDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken);
    Task<UserDto?> UpdateAsync(int id, CreateUserDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
