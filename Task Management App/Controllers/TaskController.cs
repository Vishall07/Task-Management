using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.DTOs;
using TaskManagement.Api.Services;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public ActionResult<TaskDto> Post(CreateTaskDto dto)
        {
            var result = _taskService.Create(dto);
            return StatusCode(201, result);
        }

        [HttpGet]
        public ActionResult<List<TaskDto>> GetAll()
        {
            return Ok(_taskService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<TaskDto> GetById(int id)
        {
            var task = _taskService.GetById(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPut("{id}")]
        public ActionResult<TaskDto> Update(int id, CreateTaskDto dto)
        {
            var updated = _taskService.Update(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _taskService.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
