using System.ComponentModel.DataAnnotations;

namespace Company1.ToDoApp.WebMvc.Models
{
    public class TaskViewModel
    {
        [Required]
        public string Name { get; set; }

        public int? Priority { get; set; }

        public string StatusCode { get; set; }
    }
}
