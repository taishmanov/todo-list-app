using MediatR;
using Company1.ToDoApp.Domain.Entities;

namespace Company1.ToDoApp.Application.Tasks.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskToDo>
    {
        public string Id { get; set; }
    }
}
