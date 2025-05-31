using TaskManagement.Api.Data;
using TaskManagement.Api.DTOs;
using TaskManagement.Api.Entities;
using Task = TaskManagement.Api.Entities.Task;

namespace TaskManagement.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskDbContext _context;

        public TaskService(TaskDbContext context)
        {
            _context = context;
        }

        public List<TaskDto> GetAll()
        {
            return _context.Tasks.Select(t => ToDtoStatic(t)).ToList();
        }

        private static TaskDto ToDtoStatic(TaskManagement.Api.Entities.Task task)
        {
            return new TaskDto
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = (TaskStatus)task.Status
            };
        }

        public TaskDto Create(CreateTaskDto dto)
        {
            var task = new TaskManagement.Api.Entities.Task
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Status = (Enums.TaskStatus)dto.Status
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return new TaskDto
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = (TaskStatus)task.Status
            };
        }


        public TaskDto GetById(int id)
        {
            var task = _context.Tasks.Find(id);
            return task == null ? null : ToDto(task);
        }

        public TaskDto Update(int id, CreateTaskDto dto)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return null;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate;
            task.Status = (Enums.TaskStatus)dto.Status;

            _context.SaveChanges();

            return ToDto(task);
        }

        public bool Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return true;
        }

        private TaskDto ToDto(Task task)
        {
            return new TaskDto
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = (TaskStatus)task.Status
            };
        }
    }
}
