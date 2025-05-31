using TaskManagement.Api.DTOs;

namespace TaskManagement.Api.Services
{
    public interface ITaskService
    {
        TaskDto Create(CreateTaskDto dto);
        List<TaskDto> GetAll();
        TaskDto GetById(int id);
        TaskDto Update(int id, CreateTaskDto dto);
        bool Delete(int id);
    }
}


