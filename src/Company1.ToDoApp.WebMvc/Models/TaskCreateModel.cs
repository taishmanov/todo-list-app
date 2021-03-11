using System.ComponentModel.DataAnnotations;

namespace Company1.ToDoApp.WebMvc.Models
{
    public class TaskCreateModel
    {
        [Required]
        public string Name { get; set; }
        public int? Priority { get; set; }
    }
}
