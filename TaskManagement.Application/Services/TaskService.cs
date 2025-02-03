using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper; 

    public TaskService(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetTaskDTO>> GetTasksAsync()
    {
        var tasks = await _taskRepository.GetAllTasksAsync();
        return _mapper.Map<IEnumerable<GetTaskDTO>>(tasks);
    }

    public async Task<GetTaskDTO?> GetTaskByIdAsync(int id)
    {
        var task = await _taskRepository.GetTaskByIdAsync(id);
        return task == null ? null : _mapper.Map<GetTaskDTO>(task);
    }
    public async Task CreateTaskAsync(TaskItem task) => await _taskRepository.AddTaskAsync(task);
    public async Task<TaskItem?> UpdateTaskAsync(int id, UpdateTaskDTO updateTaskDto)
    {
        var existingTask = await _taskRepository.GetTaskByIdAsync(id);
        if (existingTask == null)
            return null; 

        _mapper.Map(updateTaskDto, existingTask); 

        await _taskRepository.UpdateTaskAsync(existingTask);
        return existingTask;
    }
    public async Task DeleteTaskAsync(int id) => await _taskRepository.DeleteTaskAsync(id);
}
