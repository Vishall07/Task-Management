using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.DTOs
{
    public class CreateTaskDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public TaskStatus Status { get; set; }
    }
}