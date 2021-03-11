using MediatR;

namespace Company1.ToDoApp.Application.Tasks.Commands
{
    public class UpdateTaskCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Priority { get; set; }
        public string Status { get; set; }
    }
}
