using System.ComponentModel.DataAnnotations;

namespace Company1.ToDoApp.WebMvc.Models
{
    public class TaskDeleteModel
    {
        [Required]
        public string Name { get; set; }
    }
}
