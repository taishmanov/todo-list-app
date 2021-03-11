using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Company1.ToDoApp.WebMvc.Models
{
    public class TaskUpdateModel : TaskViewModel
    {
        [Required]
        public string CurrentName { get; set; }

        public IList<SelectListItem> AvailableStatusses { set; get; }
    }
}
