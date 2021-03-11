using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Company1.ToDoApp.WebMvc.Models
{
    public class TasksListViewModel
    {
        public IEnumerable<TaskViewModel> Items { get; set; }
        public IList<SelectListItem> AvailableStatusses { set; get; }
    }
}
