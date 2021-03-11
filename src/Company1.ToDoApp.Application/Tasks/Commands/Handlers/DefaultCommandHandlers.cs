using Company1.ToDoApp.Application.Common.Exceptions;
using Company1.ToDoApp.Domain.Entities;
using Company1.ToDoApp.Domain.ReferenceData;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Company1.ToDoApp.Application.Tasks.Commands.Handlers
{
    public class DefaultCommandHandlers : 
        IRequestHandler<CreateTaskCommand, string>,
        IRequestHandler<UpdateTaskCommand, string>,
        IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly InMemoryItemsCollection<TaskToDo> _collection;
        public DefaultCommandHandlers(InMemoryItemsCollection<TaskToDo> collection)
        {
            _collection = collection;
        }

        public Task<string> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
                throw new AppException("Task name can't be empty");

            //TODO: validate whether Task status code is correct

            lock (_collection.Lock)
            {
                if (_collection.Items.ContainsKey(request.Name))
                    throw new AppException("Task with the same name already exists");

                _collection.Items.Add(request.Name, new TaskToDo() { 
                    Name = request.Name,
                    Priority = request.Priority,
                    StatusCode = request.Statues
                });
            }

            return Task.FromResult(request.Name);
        }

        public Task<string> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
                throw new ArgumentException("Task id can't be empty");

            if (string.IsNullOrEmpty(request.Name))
                throw new AppException("Task name can't be empty");

            //TODO: validate whether Task status code is correct

            TaskToDo itemToUpdate = null;

            lock (_collection.Lock)
            {
                
                if (!request.Name.Equals(request.Id))
                {
                    if (_collection.Items.ContainsKey(request.Name))
                        throw new AppException($"Task '{request.Name}' already exists");

                    if (!_collection.Items.ContainsKey(request.Id))
                        throw new AppException($"Task '{request.Id}' doesn't exist. Please, refresh the list of tasks");

                    _collection.Items.Remove(request.Id, out itemToUpdate);
                    _collection.Items.Add(request.Name, itemToUpdate);
                }
                else
                {
                    itemToUpdate = _collection.Items[request.Id];
                }

                itemToUpdate.Name = request.Name;
                itemToUpdate.Priority = request.Priority;
                itemToUpdate.StatusCode = request.Status;
            }

            return Task.FromResult(itemToUpdate.Name);
        }

        public Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var removed = false;
            lock (_collection.Lock)
            {
                if (_collection.Items.ContainsKey(request.Id))
                {
                    if (_collection.Items[request.Id].StatusCode != TaskToDoStatus.Completed.Code)
                        throw new AppException($"Only '{TaskToDoStatus.Completed.Name}' task can be deleted");

                    _collection.Items.Remove(request.Id);
                    removed = true;
                }
                    
            }

            return Task.FromResult(removed);
        }
    }
}
