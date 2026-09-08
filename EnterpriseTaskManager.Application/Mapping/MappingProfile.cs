using AutoMapper;
using EnterpriseTaskManager.Application.DTOs.Users;
using EnterpriseTaskManager.Application.DTOs.Projects;
using EnterpriseTaskManager.Application.DTOs.Tags;
using EnterpriseTaskManager.Application.DTOs.Tasks;
using EnterpriseTaskManager.Application.DTOs.Comments;
using EnterpriseTaskManager.Domain.Entities;

namespace EnterpriseTaskManager.Application.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();

        CreateMap<Project, ProjectDto>();
        CreateMap<CreateProjectDto, Project>(); 

        CreateMap<TaskItem, TaskItemDto>();
        CreateMap<CreateTaskItemDto, TaskItem>();

        CreateMap<TaskComment, TaskCommentDto>();
        CreateMap<CreateTaskCommentDto, TaskComment>();

        CreateMap<Tag, TagDto>();
        CreateMap<CreateTagDto, TagDto>();
    }
   
}
