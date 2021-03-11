using MediatR;
using Company1.ToDoApp.Domain.Entities;
using Company1.ToDoApp.Application.Common;

namespace Company1.ToDoApp.Application.Tasks.Queries
{
    public class GetTasksListQuery : IRequest<PagedList<TaskToDo>>
    {
        
    }
}
