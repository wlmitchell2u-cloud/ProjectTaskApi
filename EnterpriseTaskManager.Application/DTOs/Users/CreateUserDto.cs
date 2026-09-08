using EnterpriseTaskManager.Application.DTOs.Users;

namespace EnterpriseTaskManager.Application.DTOs.Users
{
    public class CreateUserDto
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;
    }
}
