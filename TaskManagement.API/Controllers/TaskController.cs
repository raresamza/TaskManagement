using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;

namespace TaskManagement.API.Controllers;

[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;

    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTaskDTO>>> GetTasks()
    {
        var tasks = await _taskService.GetTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetTaskDTO>> GetTask(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        return task == null ? NotFound() : Ok(task);
    }
    //[Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateTask(TaskItem task)
    {
        await _taskService.CreateTaskAsync(task);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDTO updateTaskDto)
    {
        if (updateTaskDto == null)
            return BadRequest("Task data is required.");

        var updatedTask = await _taskService.UpdateTaskAsync(id, updateTaskDto);
        if (updatedTask == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        await _taskService.DeleteTaskAsync(id);
        return NoContent();
    }
}
