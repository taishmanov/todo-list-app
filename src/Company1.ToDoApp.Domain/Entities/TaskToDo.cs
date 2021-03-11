namespace Company1.ToDoApp.Domain.Entities
{
    public class TaskToDo
    // originally named Task, then renamed it to TaskToDo
    // to avoid conflict with System.Threading.Tasks.Task class.
    // No need to define alias type everywhere
    {
        public string Name { get; set; }
        public int? Priority { get; set; }
        public string StatusCode { get; set; }
    }
}
