using MediatR;

namespace Company1.ToDoApp.Application.Tasks.Commands
{
    public class CreateTaskCommand : IRequest<string>
    {
        public string Name { get; set; }
        public int? Priority { get; set; }
        public string Statues { get; set; }
    }
}
