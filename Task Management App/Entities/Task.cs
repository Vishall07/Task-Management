using System;
using TaskManagement.Api.Enums;

namespace TaskManagement.Api.Entities
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskManagement.Api.Enums.TaskStatus Status { get; set; }
    }
}
