using Company1.ToDoApp.Application.Common;
using Company1.ToDoApp.Application.Common.Exceptions;
using Company1.ToDoApp.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Company1.ToDoApp.Application.Tasks.Queries.Handlers
{
    public class DefaultQueryHandlers : 
        IRequestHandler<GetTaskByIdQuery, TaskToDo>,
        IRequestHandler<GetTasksListQuery, PagedList<TaskToDo>>
    {
        private readonly InMemoryItemsCollection<TaskToDo> _collection;

        public DefaultQueryHandlers(InMemoryItemsCollection<TaskToDo> collection)
        {
            _collection = collection;
        }

        public Task<TaskToDo> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            if(!_collection.Items.TryGetValue(request.Id, out var item))
                throw new NotFoundException($"Task '{request.Id}' doesn't exist.");

            return Task.FromResult(item);
        }

        public Task<PagedList<TaskToDo>> Handle(GetTasksListQuery request, CancellationToken cancellationToken)
        {
            TaskToDo[] tasks;
            lock (_collection.Lock)
            {
                tasks = new TaskToDo[_collection.Items.Count];
                _collection.Items.Values.CopyTo(tasks, 0);
            }
            return Task.FromResult(new PagedList<TaskToDo>(tasks));
        }
    }
}
