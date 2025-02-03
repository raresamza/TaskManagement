using AutoMapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Mapping;

public class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        CreateMap<TaskItem, GetTaskDTO>();

        CreateMap<GetTaskDTO, TaskItem>();

        CreateMap<UpdateTaskDTO, TaskItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); 
    }
}
