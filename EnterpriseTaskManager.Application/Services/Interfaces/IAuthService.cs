
using EnterpriseTaskManager.Application.DTOs.Auth;

namespace EnterpriseTaskManager.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(string username, string password, CancellationToken cancellationToken);
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken);
    }
}
