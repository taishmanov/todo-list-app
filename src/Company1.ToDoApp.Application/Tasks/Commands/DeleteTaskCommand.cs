using MediatR;

namespace Company1.ToDoApp.Application.Tasks.Commands
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
